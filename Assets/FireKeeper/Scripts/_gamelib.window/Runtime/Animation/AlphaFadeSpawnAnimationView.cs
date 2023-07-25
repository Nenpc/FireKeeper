using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace GameLib.Window
{
    public sealed class AlphaFadeSpawnAnimationView : SpawnAnimationView
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeAnimationTime = 0.5f;
        
        public override async UniTask ForwardAsync(CancellationToken token)
        {
            await AnimationFadeAsync(0f, 1f, token);
        }

        public override async UniTask BackwardAsync(CancellationToken token)
        {
            await AnimationFadeAsync(1f, 0f, token);
        }

        private async UniTask AnimationFadeAsync(float startAlphaValue, float endAlphaValue, CancellationToken token)
        {
            var cts = CancellationTokenSource.CreateLinkedTokenSource(token, this.GetCancellationTokenOnDestroy());
            if (!cts.IsCancellationRequested) return;
            
            _canvasGroup.alpha = startAlphaValue;
            //await _canvasGroup.DOFade(endAlphaValue, _fadeAnimationTime).WithCancellation(cts.Token);
        }
    }
}