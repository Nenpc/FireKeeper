using GameLib.Window;
using TMPro;
using UnityEngine;

namespace FireKeeper.Core.UserInterface
{
    public class DefeatPopupView : WindowView
    {
        [SerializeField] private TextMeshProUGUI _defeatText;

        public void SetText(string defeatText)
        {
            _defeatText.text = defeatText;
        }
    }
}