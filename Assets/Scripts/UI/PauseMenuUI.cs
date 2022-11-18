using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class PauseMenuUI : MonoBehaviour
    {
        private Action continueGameAction;
        public void ContinueGameActionSubscribe(Action function) => continueGameAction += function;
        public void ContinueGameActionUnsubscribe(Action function) => continueGameAction -= function;
        
        private Action mainMenuAction;
        public void MainMenuActionSubscribe(Action function) => mainMenuAction += function;
        public void MainMenuActionUnsubscribe(Action function) => mainMenuAction -= function;

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
            continueGameAction?.Invoke();
        }

        private void MainMenu()
        {
            mainMenuAction?.Invoke();
        }
    }
}
