using UnityEngine;

[CreateAssetMenu(fileName = "DifficultSetting", menuName = "GameSettings/DifficultSetting", order = 6)]
public class DifficultSetting : ScriptableObject
{
	[Range(1f, 10)]
	public int difficult;
	[Range(50f, 300)]
	public int bonfireMaxLifetime;
}
