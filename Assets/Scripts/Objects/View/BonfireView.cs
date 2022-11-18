using UnityEngine;
using Zenject;

namespace GameView
{
	public class BonfireView : MonoBehaviour
	{
		[SerializeField] private AudioSource sound;
		[SerializeField] private ParticleSystem spark;
		[SerializeField] private ParticleSystem smoke;
		[SerializeField] private ParticleSystem fire;
		[SerializeField] private ParticleSystem fireSecond;
		[SerializeField] private Light light;

		[SerializeField] private BonfireSetting setting;

		[Inject]
		private void Construct()
		{
			sound.clip = setting.FireSound;
			sound.Play();

			BonfirePower(1);

			spark.Play();
			smoke.Play();
			fire.Play();
			fireSecond.Play();
			light.gameObject.SetActive(true);
		}

		public void BonfirePower(float value)
		{
			value *= 0.01f;
			spark.startSize = setting.SparkStartSize * value;
			spark.startSpeed = setting.SparkStartSpeed * value;
			//spark.startSize = setting.SparkStartSize * value;

			smoke.startSize = setting.SmokeStartSize * value;
			smoke.startSpeed = setting.SmokeStartSpeed * value;
			//smoke.startSize = setting.SmokeStartSize * value;

			fire.startSize = setting.FireStartSize * value;
			fire.startSpeed = setting.FireStartSpeed * value;
			//fire.startSize = setting.FireStartSize * value;
			
			fireSecond.startSize = setting.FireSecondStartSize * value;
			fireSecond.startSpeed = setting.FireSecondStartSpeed * value;
			//fireSecond.startSize = setting.FireSecondStartSize * value;

			light.range = setting.LightRange * value;
			light.intensity = setting.LightIntensity * value;

			sound.volume = value;
		}

		public void FireGoOut()
		{
			sound.Stop();

			spark.Stop();
			smoke.Stop();
			fire.Stop();
			light.gameObject.SetActive(false);
		}
	}
}
