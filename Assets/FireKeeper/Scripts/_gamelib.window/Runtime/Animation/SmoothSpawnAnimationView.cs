using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace GameLib.Window
{ 
    enum SideType
    {
        Top = 0,
        Down = 1
    }

    public class SmoothSpawnAnimationView : SpawnAnimationView
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _showAnimationTime = 0.5f;
        [SerializeField] private SideType _side = SideType.Top;
        [SerializeField] private Ease _ease = Ease.OutBack;
        
        public override async UniTask ForwardAsync(CancellationToken token)
        {
            switch (_side)
            {
                case SideType.Top:
                    _rectTransform.anchoredPosition = new Vector2(
                        _rectTransform.anchoredPosition.x,
                        _rectTransform.rect.height
                    );
                    break;
                case SideType.Down:
                    _rectTransform.anchoredPosition = new Vector2(
                        _rectTransform.anchoredPosition.x,
                        -_rectTransform.rect.height);
                    break;
            }

            //await _rectTransform
            //    .DOAnchorPosY(0, _showAnimationTime)
            //    .SetEase(_ease)
            //    .WithCancellation(token);
        }

        public override async UniTask BackwardAsync(CancellationToken token)
        {
            var endValueY = 0f;
            switch (_side)
            {
                case SideType.Top:
                    endValueY = _rectTransform.rect.height;
                    break;
                case SideType.Down:
                    endValueY = -_rectTransform.rect.height;
                    break;
            }

            //await _rectTransform
            //    .DOAnchorPosY(endValueY, _showAnimationTime)
            //    .SetEase(_ease)
            //    .WithCancellation(token);
        }
    }
}