using GameLib.Window;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace FireKeeper.Core.UserInterface
{
    public sealed class CoreHudView : WindowView
    {
        [SerializeField] private Image _progressImage;
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private RectTransform _effectPanel;
        [SerializeField] private AssetReferenceGameObject _effectAsset;

        public void SetProgressInfo(float curTime, float winTime)
        {
            _progressText.text = $"{(int)curTime}/{(int)winTime}";
            var value = Mathf.Clamp(curTime / winTime, 0, 1);
            _progressImage.transform.localScale = new Vector3(value,1, 1);
        }

        public RectTransform GetEffectParent() => _effectPanel;
        public AssetReferenceGameObject GetEffectAsset() => _effectAsset;
    }
}
