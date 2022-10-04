using System;
using UnityEngine;

namespace GameView
{
	public class BonfireView : MonoBehaviour, IDisposable
	{
		[SerializeField] private AudioSource sound;
		[SerializeField] private ParticleSystem spark;
		[SerializeField] private ParticleSystem smoke;
		[SerializeField] private ParticleSystem fire;
		[SerializeField] private ParticleSystem fireSecond;
		[SerializeField] private Light light;

		[SerializeField] private BonfireSetting setting;

		public GameLogic.Bonfire bonfireLogic;

		public bool Initialize(GameLogic.Bonfire bonfireLogic)
		{
			if (sound == null)
				return false;
			if (spark == null)
				return false;
			if (smoke == null)
				return false;
			if (fire == null)
				return false;
			if (fireSecond == null)
				return false;
			if (light == null)
				return false;

			this.bonfireLogic = bonfireLogic;
			GameLogic.Bonfire.FireGoOut += FireGoOut;
			GameLogic.Bonfire.Lifetime += SetBonfireView;

			sound.clip = setting.FireSound;
			sound.Play();

			SetBonfireView(1);

			spark.Play();
			smoke.Play();
			fire.Play();
			fireSecond.Play();
			light.gameObject.SetActive(true);

			return true;
		}

		public void SetBonfireView(float value)
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

		private void FireGoOut()
		{
			sound.Stop();

			spark.Stop();
			smoke.Stop();
			fire.Stop();
			light.gameObject.SetActive(false);
		}

		public void Dispose()
		{
			GameLogic.Bonfire.FireGoOut -= FireGoOut;
			GameLogic.Bonfire.Lifetime -= SetBonfireView;
		}
	}
}
