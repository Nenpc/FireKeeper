using UnityEngine;

[CreateAssetMenu(fileName = "InputSetting", menuName = "GameSettings/InputSetting", order = 3)]
public class InputSetting : ScriptableObject
{
	public KeyCode Interaction;
	public KeyCode Drop;

	public KeyCode Forward;
	public KeyCode Back;
	public KeyCode Right;
	public KeyCode Left;

	public KeyCode Run;
}
