using UnityEngine;

public interface IInteractable
{
	bool Interactable();
}

public class BaseObject : MonoBehaviour, IInteractable
{
	public virtual bool Interactable()
	{
		return true;
	}
}
