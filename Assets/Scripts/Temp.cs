using Managers;
using UnityEngine;

public class Temp : MonoBehaviour
{
	[SerializeField] private GameObject Menu;

	bool wait;

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (wait)
				TimeManager.Instance.Continue();
			else
				TimeManager.Instance.Stop();
		}
    }
}
