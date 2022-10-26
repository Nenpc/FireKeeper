using System;
using GameLogic;
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

		public Bonfire bonfireLogic;

		[Inject]
		private void Construct(Bonfire bonfireLogic)
		{
			this.bonfireLogic = bonfireLogic;
		}

		public void Awake()
		{
			bonfireLogic.FireGoOut += FireGoOut;
			bonfireLogic.Lifetime += SetBonfireView;

			sound.clip = setting.FireSound;
			sound.Play();

			SetBonfireView(1);

			spark.Play();
			smoke.Play();
			fire.Play();
			fireSecond.Play();
			light.gameObject.SetActive(true);
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

		public void OnDestroy()
		{
			bonfireLogic.FireGoOut -= FireGoOut;
			bonfireLogic.Lifetime -= SetBonfireView;
		}
	}
}
