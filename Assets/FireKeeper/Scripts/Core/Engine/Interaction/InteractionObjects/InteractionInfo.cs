using System;

namespace FireKeeper.Core.Engine
{
    public class InteractionInfo
    {
        private readonly Action _deleteObject;
        private readonly Type _interactionType;
        private readonly Object _interactObject;

        public InteractionInfo(Object interactObject, Type interactionType, Action deleteObject = null)
        {
            _interactObject = interactObject;
            _interactionType = interactionType;
            _deleteObject = deleteObject;
        }

        public Type InteractionType 
        {
            get { return _interactionType; }
        }
        
        public Object InteractObject 
        {
            get { return _interactObject; }
        }

        public void DeleteObject()
        {
            _deleteObject?.Invoke();
        }
    }
}