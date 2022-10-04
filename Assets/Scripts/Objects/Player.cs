using GameLogic;
using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameView
{
	public enum PlayerAnimation
	{
		Run,
		Jump,
		Sprint,
		Gathering
	}

	[RequireComponent(typeof(CharacterController), typeof(Transform))]
	public class Player : MonoBehaviour, IInitialize, IDisposable
	{
		public static Action<float> staminaChanged;
		public static Action<bool> interactObjectNear;
		public static Action<Improvement> takeImprovment;

		[SerializeField] private PlayerSetting playerSetting;

		[SerializeField] private Animator animator;
		[SerializeField] private CharacterController characterController;
		[SerializeField] private Transform logPosition;

		[Header ("Jump")]
		[SerializeField] private float downSpeed = -18;
		[SerializeField] private float hitDistance = 1f;
		[SerializeField] private int hitLayer = 64;

		private Log bag;
		private Log nearestLog;

		private Improvement nearestImprovement;

		private Bonfire bonfire;
		
		private bool staminaInfinity;
		private float stamina;

		private bool isGathering;

		private bool wait;

		private float runSpeed;
		private float walkSpeed;

		public bool Initialize()
		{
			InputManager.Instance.GoFront += GoFront;
			InputManager.Instance.GoRight += GoRight;
			InputManager.Instance.GoLeft += GoLeft;
			InputManager.Instance.GoBack += GoBack;
			InputManager.Instance.Sprint += Sprint;
			InputManager.Instance.Interaction += Interaction;
			InputManager.Instance.Drop += Drop;

			stamina = playerSetting.maxStamina;
			runSpeed = playerSetting.runSpeed;
			walkSpeed = playerSetting.walkSpeed;
			TimeManager.Instance.Tiking += ImprovementTimeCounter;
			TimeManager.Instance.StopAction += StopGame;
			TimeManager.Instance.ContinueAction += ContinueGame;
			return true;
		}

		#region InputMethodValue
		private bool goFront;
		private bool goRight;
		private bool goLeft;
		private bool goBack;
		private bool sprint;
		#endregion

		#region InputMethodHandler
		private void GoFront(bool value) { goFront = value;}
		private void GoRight(bool value) { goRight = value; }
		private void GoLeft(bool value) { goLeft = value; }
		private void GoBack(bool value) { goBack = value; }
		private void Sprint(bool value) { sprint = value; }
		#endregion

		public void Dispose()
		{
			TimeManager.Instance.StopAction -= StopGame;
			TimeManager.Instance.ContinueAction -= ContinueGame;

			InputManager.Instance.GoFront -= GoFront;
			InputManager.Instance.GoRight -= GoRight;
			InputManager.Instance.GoLeft -= GoLeft;
			InputManager.Instance.GoBack -= GoBack;
			InputManager.Instance.Sprint -= Sprint;
			InputManager.Instance.Interaction -= Interaction;
			InputManager.Instance.Drop -= Drop;
		}

		private void StopGame()
		{
			wait = true;
		}

		private void ContinueGame()
		{
			wait = false;
		}

		private bool OnGround()
		{
			if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, hitDistance, hitLayer))
			{
				return true;
			}
			return false;
		}

		private void Update()
		{
			if (wait)
				return;

			Vector3 direction = Vector3.zero;

			if (goFront) direction += transform.forward;

			if (goRight) direction += transform.right;

			if (goLeft) direction += transform.right * (-1);

			if (goBack) direction += transform.forward * (-1);

			direction = direction.normalized;

			if (sprint && stamina > 0 && direction != Vector3.zero)
			{
				direction *= runSpeed;
				animator.SetBool(PlayerAnimation.Sprint.ToString(), true);
				stamina = Mathf.Clamp(stamina - Time.deltaTime, 0, playerSetting.maxStamina); 
			}
			else
			{
				direction *= walkSpeed;
				animator.SetBool(PlayerAnimation.Run.ToString(), true);
				animator.SetBool(PlayerAnimation.Sprint.ToString(), false);
				stamina = Mathf.Clamp(stamina + Time.deltaTime, 0, playerSetting.maxStamina); ;
			}
			
			if (isGathering)
				direction = Vector3.zero;

			if (direction == Vector3.zero)
			{
				animator.SetBool(PlayerAnimation.Sprint.ToString(), false);
				animator.SetBool(PlayerAnimation.Run.ToString(), false);
			}
			else
			{
				RotatePlayer(direction);
				animator.SetBool(PlayerAnimation.Run.ToString(), true);
			}

			var ySpeed = 0f;
			if (!OnGround())
			{
				ySpeed = downSpeed * Time.deltaTime;
			}

			staminaChanged?.Invoke(stamina);
			characterController.Move(new Vector3(direction.x, ySpeed, direction.z) * Time.deltaTime);
		}

		private readonly Quaternion wrongRotation = new Quaternion(1, 0, 0, 0);
		private readonly Quaternion rightRotation = new Quaternion(0, 1, 0, 0);
		
		private void RotatePlayer(Vector3 destination)
		{
			var rotation = Quaternion.FromToRotation(Vector3.forward, destination);
			// Небольшой костыль, в Jira добавил
			if (rotation == wrongRotation)
				rotation = rightRotation;

			animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, rotation, 0.05f);
		}

		private void Drop()
		{
			if (bag != null)
			{
				bag.Drop();
				bag = null;
			}
		}

		private void Interaction()
		{
			if (bonfire != null)
			{
				if (bag != null)
				{
					StartCoroutine(EndGatheringAnimation());
					BurnLog();
				}
				else
				{
					Debug.Log("I need log");
				}
			}

			if (bag == null && nearestLog != null)
			{
				if (nearestLog.Take(out bag))
				{
					StartCoroutine(EndGatheringAnimation());
					GetLog();
				}
			}

			if (nearestImprovement != null)
			{
				UseImprovement(nearestImprovement.Use());
				interactObjectNear?.Invoke(false);
				nearestImprovement = null;
			}
		}

		private IEnumerator EndGatheringAnimation()
		{
			isGathering = true;
			animator.SetBool(PlayerAnimation.Gathering.ToString(), true);
			yield return new WaitForSeconds(2.1f);
			animator.SetBool(PlayerAnimation.Gathering.ToString(), false);
			isGathering = false;
		}

		private void BurnLog()
		{
			bonfire.AddLog(bag.logCapacity);
			bag.Burn();
			bag = null;
			interactObjectNear?.Invoke(false);
		}

		private void GetLog()
		{
			bag.transform.SetParent(logPosition);
			bag.transform.localPosition = Vector3.zero;
			bag.transform.localRotation = Quaternion.identity;
			interactObjectNear?.Invoke(false);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.TryGetComponent(out LogView log))
			{
				if (!log.IsTake && bag == null)
				{
					nearestLog = log.logic;
					interactObjectNear?.Invoke(true);
				}
			}

			if (other.gameObject.TryGetComponent(out BonfireView bonfireView))
			{
				bonfire = bonfireView.bonfireLogic;
				interactObjectNear?.Invoke(true);
			}

			if (other.gameObject.TryGetComponent(out ImprovementView improvementView))
			{
				nearestImprovement = improvementView.logic;
				interactObjectNear?.Invoke(true);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.TryGetComponent(out LogView log))
			{
				if (bag == log.logic)
				{
					interactObjectNear?.Invoke(false);
				}
				else if (nearestLog == log.logic)
				{
					nearestLog = null;
					interactObjectNear?.Invoke(false);
				}
			}

			if (other.gameObject.TryGetComponent(out BonfireView bonfireView))
			{
				bonfire = null;
				interactObjectNear?.Invoke(false);
			}
			
			if (other.gameObject.TryGetComponent(out ImprovementView improvementView))
			{
				nearestImprovement = null;
				interactObjectNear?.Invoke(false);
			}
		}

		private List<Improvement> improvementList = new List<Improvement>(3);

		private void UseImprovement(Improvement improvment)
		{
			Debug.Log("Improvements Used");
			
			// if (improvementList.Count == improvementList.Capacity)
			// 	return;

			switch (improvment.improvementType)
			{
				case ImprovementType.SpeedUp:
					Debug.Log("ImprovementType.SpeedUp " + improvment.Time);
					improvementList.Add(improvment);
					runSpeed += improvment.Capacity;
					takeImprovment?.Invoke(improvment);
					break;
				case ImprovementType.StaminaRecovery:
					Debug.Log("ImprovementType.StaminaRecovery " + improvment.Time);
					stamina += improvment.Capacity;
					break;
				case ImprovementType.StaminaInfinite:
					Debug.Log("ImprovementType.StaminaInfinite " + improvment.Time);
					staminaInfinity = true;
					improvementList.Add(improvment);
					takeImprovment?.Invoke(improvment);
					break;
				case ImprovementType.NoStorm:
					Debug.Log("ImprovementType.NoStorm " + improvment.Time);
					improvementList.Add(improvment);
					takeImprovment?.Invoke(improvment);
					break;
			}
		}

		private void ImprovementOff(Improvement improvment)
		{
			switch (improvment.improvementType)
			{
				case ImprovementType.SpeedUp:
					Debug.Log("ImprovementType.SpeedUp end " + improvment.Time);
					runSpeed -= improvment.Capacity;
					break;
				case ImprovementType.StaminaInfinite:
					Debug.Log("ImprovementType.StaminaInfinite end " + improvment.Time);
					staminaInfinity = false;
					break;
				case ImprovementType.NoStorm:
					Debug.Log("ImprovementType.NoStorm end " + improvment.Time);
					break;
			}
			improvementList.Remove(improvment);
		}

		private void ImprovementTimeCounter(int time)
		{
			for (int i = 0; i < improvementList.Count; i++)
			{
				improvementList[i].SubtractTime();
				if (improvementList[i].Time <= 0)
				{
					ImprovementOff(improvementList[i]);
					i--;
				}
			}
		}
	}
}
