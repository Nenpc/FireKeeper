using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace GameLib.Window
{
    public sealed class WindowFacade : IWindowFacade, IDisposable
    {
        public event Action<IWindowView> OnInstance;
        public event Action<IWindowView> OnRelease;
        public event Action<IWindowView> OnShow;
        public event Action<IWindowView> OnHide;

        private readonly Canvas _mainCanvas;
        private readonly WindowConfig _windowConfig;

        private readonly Dictionary<string, IWindowView> _instanceWindowViews = new Dictionary<string, IWindowView>();

        public WindowFacade(Canvas mainCanvas, WindowConfig windowConfig)
        {
            _mainCanvas = mainCanvas;
            _windowConfig = windowConfig;
        }

        public void Dispose()
        {
            OnInstance = null;
            OnRelease = null;
            OnShow = null;
            OnHide = null;
            _instanceWindowViews.Clear();
        }
        
        private async UniTask<IWindowView> InstantiateAsync(IWindowDefinition windowDefinition)
        {
            _instanceWindowViews.Add(windowDefinition.WindowType, default);//for check async
            
            var assetReference = windowDefinition.AssetReferencePrefab;
            var gameObject = await assetReference.InstantiateAsync(
                Vector3.zero,
                Quaternion.identity,
                _mainCanvas.transform
            );

            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            var windowView = gameObject.GetComponent<IWindowView>();
            AddedCanvas(windowView, windowDefinition.Order);

            gameObject.SetActive(false);
            _instanceWindowViews[windowDefinition.WindowType] = windowView;

            OnInstance?.Invoke(windowView);
            return windowView;
        }

        public async UniTask InitializeAsync()
        {
            foreach (var windowDefinition in _windowConfig.Definitions)
            {
                var initializeState = windowDefinition.InitializeState;
                if (initializeState == WindowInitializeState.None) continue;

                var windowView = await InstantiateAsync(windowDefinition);
                windowView.GameObject.SetActive(false);
            }

            UniTask.WhenAll(GetShowAsyncWindowUniTasks()).Forget();
        }

        private IEnumerable<UniTask> GetShowAsyncWindowUniTasks()
        {
            foreach (var keyValuePair in _instanceWindowViews)
            {
                var windowType = keyValuePair.Key;
                var windowView = keyValuePair.Value;

                var windowDefinition = _windowConfig.GetDefinition(windowType);
                if (windowDefinition.InitializeState != WindowInitializeState.InstanceAndShow) continue;

                yield return ShowAsyncWindow(windowView);
            }
        }

        public async UniTask<T> ShowAsync<T>(Action<T> preShowCallback = default) where T : IWindowView
        {
            if (!TryGetView<T>(out var windowView))
            {
                var windowDefinition = _windowConfig.GetDefinition<T>();
                windowView = (T)await InstantiateAsync(windowDefinition);
            }

            preShowCallback?.Invoke(windowView);
            
            await ShowAsyncWindow(windowView);

            return windowView;
        }
        
        private async UniTask ShowAsync(IWindowDefinition windowDefinition)
        {
            _instanceWindowViews.TryGetValue(windowDefinition.WindowType, out var windowView);
            
            if (windowView == null) windowView = await InstantiateAsync(windowDefinition);

            await ShowAsyncWindow(windowView);
        }

        private async UniTask ShowAsyncWindow(IWindowView windowView)
        {
            windowView.GameObject.SetActive(true);
            OnShow?.Invoke(windowView);
            await windowView.ShowAsync();
        }
        
        public async UniTask HideAsync<T>() where T : IWindowView
        {
            var result = TryGetView<T>(out var windowView);
            var type = typeof(T);

            if (!result)
            {
                Debug.LogError($"Can't find exist view for {type}");
                return;
            }
            
            var windowDefinition = _windowConfig.GetDefinition<T>();
            await HideAsyncWindow(windowView, windowDefinition);
        }
        
        private async UniTask HideAsync(IWindowDefinition windowDefinition)
        {
            var result = _instanceWindowViews.TryGetValue(windowDefinition.WindowType, out var windowView);
            
            if (!result)
            {
                Debug.LogError($"Can't find exist view for {windowDefinition}");
                return;
            }

            await HideAsyncWindow(windowView, windowDefinition);
        }
        
        private async UniTask HideAsyncWindow(IWindowView windowView, IWindowDefinition windowDefinition)
        {
            OnHide?.Invoke(windowView);
        
            await windowView.HideAsync();
            windowView.GameObject.SetActive(false);
            
            if (windowDefinition.HideState == WindowHideState.CloseAndRelease)
            {
                _instanceWindowViews.Remove(windowView.GetType().ToString());

                OnRelease?.Invoke(windowView);
                Addressables.ReleaseInstance(windowView.GameObject);
            }
        }

        public bool TryGetView<T>(out T view) where T : IWindowView
        {
            var type = typeof(T);
            var result = _instanceWindowViews.TryGetValue(type.ToString(), out var windowView);

            if (result) view = (T)windowView;
            else view = default;

            return result;
        }
        
        public bool TryGetView(string windowId, out IWindowView view)
        {
            var windowDefinitionType = _windowConfig.GetDefinition(windowId).WindowType;
            
            return _instanceWindowViews.TryGetValue(windowDefinitionType.ToString(), out view);
        }

        public bool HasView<T>() where T : IWindowView
        {
            var result = TryGetView<T>(out  var view);
            return result && view != null;
        }

        private void AddedCanvas(IWindowView windowView, int order)
        {
            var gameObject = windowView.GameObject;
            
            var canvas = gameObject.AddComponent<Canvas>();
            canvas.overrideSorting = true;
            canvas.sortingOrder = order;

            gameObject.AddComponent<GraphicRaycaster>();
        }
        
        public async UniTask VisibleAsync(IWindowDefinition windowDefinition, bool visible)
        {
            if (visible) await ShowAsync(windowDefinition);
            else await HideAsync(windowDefinition);
        }

    }
}