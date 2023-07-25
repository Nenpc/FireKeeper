using GameLib.Window;
using TMPro;
using UnityEngine;

namespace FireKeeper.Core.UserInterface
{
    public sealed class WinPopupView : WindowView
    {
        [SerializeField] private TextMeshProUGUI _winText;

        public void SetText(string winText)
        {
            _winText.text = winText;
        }
    }
}