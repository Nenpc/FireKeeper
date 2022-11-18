using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class PauseMenuUI : MonoBehaviour
    {
        public event Action ContinueGameEvent;
        public event Action MainMenuEvent;

        [SerializeField] private Button continueButton;
        [SerializeField] private Button mainMenuButton;

        private void Awake()
        {
            continueButton.onClick.AddListener(RestartGame);
            mainMenuButton.onClick.AddListener(MainMenu);
        }

        private void OnDestroy()
        {
            continueButton.onClick.RemoveListener(RestartGame);
            mainMenuButton.onClick.RemoveListener(MainMenu);
        }

        public void ShowScreen()
        {
            gameObject.SetActive(true);
        }

        public void HideScreen()
        {
            gameObject.SetActive(false);
        }

        private void RestartGame()
        {
            ContinueGameEvent?.Invoke();
        }

        private void MainMenu()
        {
            MainMenuEvent?.Invoke();
        }
    }
}
