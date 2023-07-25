using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Startup
{
    public sealed class LoadingScreenView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public void ShowAsync()
        {
            gameObject.SetActive(true);
        }

        public void HideAsync()
        {
            gameObject.SetActive(false);
        }
    }
}