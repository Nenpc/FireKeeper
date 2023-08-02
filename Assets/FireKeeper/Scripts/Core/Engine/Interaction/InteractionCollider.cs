using System;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    [RequireComponent(typeof(Collider))]
    public sealed class InteractionCollider : MonoBehaviour
    {
        public event Action<Collider> FindedObjectAction;
        public event Action<Collider> LosedObjectAction;
        
        public void OnTriggerEnter(Collider other)
        {
            FindedObjectAction?.Invoke(other);
        }
        
        public void OnTriggerExit(Collider other)
        {
            LosedObjectAction?.Invoke(other);
        }
    }
}