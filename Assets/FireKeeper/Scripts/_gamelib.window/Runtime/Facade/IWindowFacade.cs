using System;
using Cysharp.Threading.Tasks;

namespace GameLib.Window
{
    public interface IWindowFacade
    {
        event Action<IWindowView> OnInstance;
        event Action<IWindowView> OnRelease;
        event Action<IWindowView> OnShow;
        event Action<IWindowView> OnHide;

        UniTask InitializeAsync();

        UniTask<T> ShowAsync<T>(Action<T> preShowCallback = default) where T : IWindowView;
        UniTask HideAsync<T>() where T : IWindowView;
        
        bool TryGetView<T>(out T view) where T : IWindowView;
        bool TryGetView(string windowId, out IWindowView view);
        bool HasView<T>() where T : IWindowView;
        
        UniTask VisibleAsync(IWindowDefinition windowDefinition, bool visible);
    }
}