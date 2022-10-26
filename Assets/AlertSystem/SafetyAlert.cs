using UnityEngine;

namespace AdditionalFunctions
{
	public class SafetyAlert : MonoBehaviour
	{
		private const string headerText = "SAFETY WARNING";
		private const string mainText = "We kindly remind you of the importance of parental supervision during using the app by little kids. Please be aware of physical hazards in the real world.";
		private const string buttonText = "Proceed to the App";

		void Start()
		{
#if UNITY_ANDROID
			CheckSafetyAlert();
#endif
		}

		void CheckSafetyAlert()
		{
			if (!PlayerPrefs.HasKey("safetyAlertShownOnce"))
			{
				//AlertSystem.Instance.ShowMessageOk(mainText, buttonText, headerText);
				PlayerPrefs.SetInt("safetyAlertShownOnce", 1);
			}
		}
	}
}
