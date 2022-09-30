using UnityEngine;

namespace GameView
{
	public class CameraPosition : MonoBehaviour
	{
		[SerializeField] private Transform camera;
		[Range (0,50)]
		[SerializeField] private float dead;
		[Range(0.01f, 5)]
		[SerializeField] private float mouseSpeed;
		[SerializeField] private bool inverting;

		private float prevMousepositionX;

		private bool wait;

		public bool Initialize()
		{
			prevMousepositionX = Input.mousePosition.x;
			return true;
		}

		private void StopGame()
		{
			wait = true;
		}

		private void ContinueGame()
		{
			wait = false;
		}

		private void Update()
		{
			if (wait)
				return;

			var delta = Input.mousePosition.x - prevMousepositionX;
			if (Mathf.Abs(delta) > dead)
			{
				if (inverting)
				{
					transform.localRotation = Quaternion.Euler(new Vector3(
						transform.localRotation.eulerAngles.x,
						transform.localRotation.eulerAngles.y - (delta * mouseSpeed),
						transform.localRotation.eulerAngles.z));
				}
				else
				{
					transform.localRotation = Quaternion.Euler(new Vector3(
						transform.localRotation.eulerAngles.x,
						transform.localRotation.eulerAngles.y + (delta * mouseSpeed),
						transform.localRotation.eulerAngles.z));
				}
			}

			prevMousepositionX = Input.mousePosition.x;
		}
	}
}
