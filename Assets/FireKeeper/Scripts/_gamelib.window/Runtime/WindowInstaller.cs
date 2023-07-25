using UnityEngine;
using Zenject;

namespace GameLib.Window
{
    public class WindowInstaller : MonoInstaller
    {
        [SerializeField] private WindowConfig _windowConfig;
        [SerializeField] private Canvas _mainCanvas;

        public override void InstallBindings()
        {
            Container.BindInstance(_windowConfig);
            Container.BindInterfacesTo<WindowFacade>().AsSingle().WithArguments(_mainCanvas);
        }
    }
}