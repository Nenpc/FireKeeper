using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace MainMenu
{

    [RequireComponent(typeof(Toggle))]
    public class LevelItem : MonoBehaviour, IDisposable
    {
        public Action<SceneSetting> selectedScene;

        [SerializeField] private Image levelIcon;
        [SerializeField] private TextMeshProUGUI levelName;
        
        private SceneSetting sceneSetting;
        private Toggle toggle;

        public void Initialize(SceneSetting sceneSetting, ToggleGroup toggleGroup)
        {
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(SelectLocation);
            toggle.group = toggleGroup;
            
            this.sceneSetting = sceneSetting;
            levelIcon.sprite = sceneSetting.sceneIcon;
            levelName.text = sceneSetting.sceneName.ToString();
        }

        public void Dispose()
        {
            this.sceneSetting = null;
            levelIcon.sprite = null;
            levelName.text = "";
            toggle.onValueChanged.RemoveListener(SelectLocation);
            toggle.group = null;
            toggle = null;
        }

        private void SelectLocation(bool value)
        {
            if (value)
                selectedScene?.Invoke(sceneSetting);
        }
    }
}
