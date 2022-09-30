using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSetting", menuName = "GameSettings/PlayerSetting", order = 7)]
public class PlayerSetting : ScriptableObject
{
	[Range(10f, 100f)]
	public float maxStamina;

	[Range(1f, 5f)]
	public float walkSpeed;
	[Range(2f, 10f)]
	public float runSpeed;

	private void OnValidate()
	{
		if (runSpeed <= walkSpeed)
			Debug.Log("<color=red>Attention! Running speed is less than walking speed.</color>");
	}
}
