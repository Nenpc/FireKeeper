using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameLib.Window
{
    public abstract class WindowController<TWindow> : IDisposable
        where TWindow : IWindowView
    {
        protected readonly IWindowFacade WindowFacade;

        protected TWindow Window { get; private set; }

        protected readonly CancellationTokenSource Cts = new CancellationTokenSource();
        protected CancellationTokenSource CtsInstance;
        protected CancellationTokenSource CtsShow;

        protected WindowController(IWindowFacade windowFacade)
        {
            WindowFacade = windowFacade;

            WindowFacade.OnInstance += OnInstanceHandler;
            WindowFacade.OnRelease += OnReleaseHandler;
            WindowFacade.OnShow += OnShowHandler;
            WindowFacade.OnHide += OnHideHandler;
        }

        public void Dispose()
        {
            OnDispose();

            WindowFacade.OnInstance -= OnInstanceHandler;
            WindowFacade.OnRelease -= OnReleaseHandler;
            WindowFacade.OnShow -= OnShowHandler;
            WindowFacade.OnHide -= OnHideHandler;

            CancelAndDisposeCts(CtsShow);
            CancelAndDisposeCts(CtsInstance);
            CancelAndDisposeCts(Cts);
        }

        protected async UniTask<TWindow> ShowWindowAsync(Action<TWindow> preShowCallback = default)
        {
            return await WindowFacade.ShowAsync(preShowCallback);
        }

        protected void ShowWindow(Action<TWindow> preShowCallback = default)
        {
            ShowWindowAsync(preShowCallback).WithCancellation(Cts.Token).Forget();
        }

        protected async UniTask HideWindowAsync()
        {
            await WindowFacade.HideAsync<TWindow>();
        }

        protected void HideWindow()
        {
            HideWindowAsync().WithCancellation(Cts.Token).Forget();
        }

        private void OnInstanceHandler(IWindowView windowView)
        {
            if (windowView is TWindow windowViewT)
            {
                CtsInstance = new CancellationTokenSource();

                Window = windowViewT;
                OnInstanceWindowSubscribe(windowViewT);
            }
        }

        private void OnReleaseHandler(IWindowView windowView)
        {
            if (windowView is TWindow windowViewT)
            {
                CancelAndDisposeCts(CtsShow);
                CancelAndDisposeCts(CtsInstance);

                OnReleaseWindowSubscribe(windowViewT);
                Window = default;
            }
        }

        private void OnShowHandler(IWindowView windowView)
        {
            CtsShow = new CancellationTokenSource();
            
            UniTask.Create(async () =>
            {
                await UniTask.Delay(1);

                if (windowView is TWindow windowViewT)
                {
                    OnShowWindowSubscribe(windowViewT);
                }
            }).WithCancellation(CtsShow.Token).Forget();
        }

        protected virtual void OnHideHandler(IWindowView windowView)
        {
            if (windowView is TWindow windowViewT)
            {
                CancelAndDisposeCts(CtsShow);

                OnHideWindowUnsubscribe(windowViewT);
            }
        }

        protected bool HasView()
        {
            return WindowFacade.HasView<TWindow>();
        }

        protected virtual void OnDispose() { }

        protected virtual void OnInstanceWindowSubscribe(TWindow window) { }

        protected virtual void OnReleaseWindowSubscribe(TWindow window) { }

        protected virtual void OnShowWindowSubscribe(TWindow window) { }

        protected virtual void OnHideWindowUnsubscribe(TWindow window) { }

        private void CancelAndDisposeCts(CancellationTokenSource cts)
        {
            if (cts is { IsCancellationRequested: false })
            {
                cts.Cancel();
                cts.Dispose();
            }
        }
    }
}