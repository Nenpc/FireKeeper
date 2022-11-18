using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinMenuUI : MonoBehaviour
{
    private event Action MainMenuEvent;

    [SerializeField] private TextMeshProUGUI nextLevelText;
    [SerializeField] private TextMeshProUGUI nextLevelNameText;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(MainMenu);
    }

    private void OnDestroy()
    {
        mainMenuButton.onClick.RemoveListener(MainMenu);
    }

    public void ShowScreen(string nextLevelName = null)
    {
        if (nextLevelName != null)
        {
            nextLevelText.gameObject.SetActive(true);
            nextLevelNameText.text = nextLevelName;
            nextLevelNameText.gameObject.SetActive(true);
        }
        else
        {
            nextLevelText.gameObject.SetActive(false);
            nextLevelNameText.gameObject.SetActive(false);
        }
    }

    public void HideScreen()
    {
        gameObject.SetActive(false);
    }

    private void MainMenu()
    {
        MainMenuEvent?.Invoke();
    }
}
