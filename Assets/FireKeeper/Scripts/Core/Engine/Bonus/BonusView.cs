using System;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public sealed class BonusView : MonoBehaviour
    {
        public event Action<Collider> OnTriggerEnterAction;
        
        public BonusController BonusController { get; private set; }

        public void Initialize(BonusController bonusController)
        {
            BonusController = bonusController;
        }

        public void OnTriggerEnter(Collider other)
        {
            OnTriggerEnterAction?.Invoke(other);
        }
    }
}