using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FireKeeper.Core.Engine;
using GameLib.Window;
using UnityEngine;

namespace FireKeeper.Core.UserInterface
{
    public sealed class CoreHudController : WindowController<CoreHudView>
    {
        private readonly IPlayerController _playerController;
        private readonly IWinController _winController;
        private readonly ITextureProvider _textureProvider;
        private readonly Dictionary<EffectTimer, CoreHudEffectElement> _effectElements;
        
        private AddressablePool<CoreHudEffectElement> _viewPool;
        private const string PoolType = "CoreHudEffectElement";

        public CoreHudController(IWindowFacade windowFacade,
            IPlayerController playerController,
            ITextureProvider textureProvider,
            IWinController winController) : base(windowFacade)
        {
            _playerController = playerController;
            _textureProvider = textureProvider;
            _winController = winController;
            _effectElements = new Dictionary<EffectTimer, CoreHudEffectElement>();

        }

        protected override void OnInstanceWindowSubscribe(CoreHudView window)
        {
            _winController.ProgressAction += WinProgress;
            _playerController.AddEffectAction += AddEffect;
            _playerController.TimeUpdateEffectAction += TimeEffectUpdate;
            _playerController.RemoveEffectAction += EffectRemove;
            _viewPool = new AddressablePool<CoreHudEffectElement>(PoolType, Window.GetEffectParent());
        }
        
        protected override void OnReleaseWindowSubscribe(CoreHudView window)
        {
            _winController.ProgressAction -= WinProgress;
            _playerController.AddEffectAction -= AddEffect;
            _playerController.TimeUpdateEffectAction -= TimeEffectUpdate;
            _playerController.RemoveEffectAction -= EffectRemove;
        }
        
        private void AddEffect(EffectTimer effect)
        {
            AddEffectAsync(effect).Forget();
        }
        
        private async UniTask AddEffectAsync(EffectTimer effectTimer)
        {
            var effectElement = await _viewPool.Get(Window.GetEffectAsset(), Vector3.zero);

            if (effectElement == null)
            {
                Debug.Log("No CoreHudEffectElement for game object");
                return;
            }

            await _textureProvider.SetIcon(effectElement.Image, effectTimer.GetEffect().GetIconKey());
            effectElement.SetProgress(0);
                
            _effectElements.Add(effectTimer, effectElement);
        }

        private void TimeEffectUpdate(EffectTimer effectTime)
        {
            _effectElements.TryGetValue(effectTime, out var effectElement);
            
            if (effectElement == null) return;

            var leftTime = effectTime.GetLeftTime();
            var maxTime = effectTime.GetMaxLeftTime();
            effectElement.SetProgress(leftTime/maxTime);
        }
        
        private void EffectRemove(EffectTimer effectTime)
        {
            _effectElements.TryGetValue(effectTime, out var effectElement);
            
            if (effectElement == null) return;
            
            _viewPool.Return(effectElement);
        }

        private void WinProgress(float curTime)
        {
            var winTime = _winController.GetWinTime();
            Window.SetProgressInfo(curTime, winTime);
        }
    }
}
