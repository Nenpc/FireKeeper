using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
	[SerializeField] private Button closeButton;
	[SerializeField] private Button upResultButton;
	[SerializeField] private Button downResultButton;

	[SerializeField] private Transform resultPanel;
	[SerializeField] private Transform resultRowPrefab;

	private void Awake()
	{
		if (closeButton != null)
			closeButton.onClick.AddListener(CloseClick);
		else
			Debug.LogError("Initialization error close button not set!");

		if (upResultButton != null)
			upResultButton.onClick.AddListener(UpResultClick);
		else
			Debug.LogError("Initialization error up result button not set!");

		if (downResultButton != null)
			downResultButton.onClick.AddListener(DownResultClick);
		else
			Debug.LogError("Initialization error down result button not set!");


		if (resultPanel == null)
			Debug.LogError("Initialization error result panel not set!");

		if (resultRowPrefab == null)
			Debug.LogError("Initialization error result row prefab not set!");

	}

	private void DownResultClick()
	{

	}

	private void UpResultClick()
	{

	}

	private void CloseClick()
	{
		gameObject.SetActive(false);
	}
	
	private void OnDestroy()
	{
		if (closeButton != null)
			closeButton.onClick.RemoveListener(CloseClick);
		else
			Debug.LogError("Initialization error close button not set!");

		if (upResultButton != null)
			upResultButton.onClick.RemoveListener(UpResultClick);
		else
			Debug.LogError("Initialization error up result button not set!");

		if (downResultButton != null)
			downResultButton.onClick.RemoveListener(DownResultClick);
		else
			Debug.LogError("Initialization error down result button not set!");
	}
}
