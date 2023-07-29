using UnityEngine;
using UnityEngine.UI;

namespace FireKeeper.Core.UserInterface
{
    public sealed class CoreHudEffectElement : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Image _goodProgressImage;
        [SerializeField] private Image _badProgressImage;

        public Image Image => _image;

        public void Initialize(bool isGoodEffect)
        {
            _goodProgressImage.transform.localScale = new Vector3(0,1, 1);
            _badProgressImage.transform.localScale = new Vector3(0,1, 1);
            _goodProgressImage.gameObject.SetActive(isGoodEffect);
            _badProgressImage.gameObject.SetActive(!isGoodEffect);
        }

        public void SetProgress(float _progress)
        {
            var value = Mathf.Clamp(_progress, 0, 1);
            _goodProgressImage.transform.localScale = new Vector3(value,1, 1);
            _badProgressImage.transform.localScale = new Vector3(value,1, 1);
        }
    }
}