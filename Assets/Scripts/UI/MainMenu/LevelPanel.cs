using Managers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainMenu
{

    public class LevelPanel : MonoBehaviour
    {
        [SerializeField] private LevelItem levelItemPrefab;
        [SerializeField] private Transform panel;
        [SerializeField] private SceneManagerSetting sceneManagerSetting;
        [SerializeField] private ToggleGroup toggleGroup;
        [SerializeField] private Button startGame;

        private SceneSetting selectedScene;
        
        private SceneSwitcher sceneSwitcher;

        [Inject]
        private void Construct(SceneSwitcher sceneSwitcher)
        {
            this.sceneSwitcher = sceneSwitcher;
        }

        private void Awake()
        {
            foreach (var scene in sceneManagerSetting.scenes)
            {
                if (scene.sceneName != SceneName.Menu)
                {
                    var scenePref = Instantiate(levelItemPrefab, panel).GetComponent<LevelItem>();
                    scenePref.Initialize(scene, toggleGroup);
                    scenePref.selectedScene += SelectedScene;
                }
            }

            startGame.onClick.AddListener(StartLevel);
        }

        private void StartLevel()
        {
            if (selectedScene != null)
                sceneSwitcher.LoadScene(selectedScene.sceneName);
        }

        private void SelectedScene(SceneSetting sceneSetting)
        {
            selectedScene = sceneSetting;
        }
    }
}
