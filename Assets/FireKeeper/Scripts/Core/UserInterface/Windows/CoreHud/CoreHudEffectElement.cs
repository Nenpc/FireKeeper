using UnityEngine;
using UnityEngine.UI;

namespace FireKeeper.Core.UserInterface
{
    public sealed class CoreHudEffectElement : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Image _progressImage;

        public Image Image => _image;

        public void SetProgress(float _progress)
        {
            var value = Mathf.Clamp(_progress, 0, 1);
            _progressImage.transform.localScale = new Vector3(value,1, 1);
        }
    }
}