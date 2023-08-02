using System;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    [RequireComponent(typeof(BoxCollider))]
    public class InteractionMono : MonoBehaviour
    {
        [SerializeField] private BoxCollider _boxCollider;

        private InteractionInfo _interactionInfo;

        public Type GetInteractionType()
        {
            if (_interactionInfo != null)
                return _interactionInfo.InteractionType;

            return null;
        }
        
        public InteractionInfo GetInteractionInfo()
        {
            return _interactionInfo;
        }

        public void Initialize(InteractionInfo interactionInfo)
        {
            _interactionInfo = interactionInfo;
        }

        public BoxCollider BoxCollider
        {
            get { return _boxCollider; }
        }
    }
}