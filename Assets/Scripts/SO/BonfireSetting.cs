using UnityEngine;

[CreateAssetMenu(fileName = "BonfireSetting", menuName = "GameSettings/BonfireSetting", order = 1)]
public class BonfireSetting : ScriptableObject
{
	[Header("Light")]
	[Range(0.5f, 20)]
	public float LightRange;
	[Range(0.5f, 5)]
	public float LightIntensity;

	[Header("Spark")]
	[Range(0.5f, 5)]
	public float SparkStartLifetime;
	[Range(0.5f, 5)]
	public float SparkStartSpeed;
	[Range(0.1f, 2)]
	public float SparkStartSize;

	[Header("Smoke")]
	[Range(0.5f, 5)]
	public float SmokeStartLifetime;
	[Range(0.5f, 5)]
	public float SmokeStartSpeed;
	[Range(0.1f, 2)]
	public float SmokeStartSize;

	[Header("Fire")]
	[Range(0.5f, 5)]
	public float FireStartLifetime;
	[Range(0.5f, 5)]
	public float FireStartSpeed;
	[Range(0.1f, 2)]
	public float FireStartSize;

	[Header("Sound")]
	public AudioClip FireSound;
}
