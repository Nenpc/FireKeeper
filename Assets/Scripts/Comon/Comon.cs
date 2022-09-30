using System;
using UnityEngine;

enum Difficult
{
	Simple,
	Normal,
	Hard
}

interface IInitialize
{
	bool Initialize();
}

public abstract class BaseGameManager : MonoBehaviour, IInitialize, IDisposable
{
	public abstract void Dispose();
	public abstract bool Initialize();
	public abstract string ManagerName();
}
