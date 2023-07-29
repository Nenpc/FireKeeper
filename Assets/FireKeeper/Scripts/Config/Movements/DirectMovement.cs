using UnityEngine;

namespace FireKeeper.Config
{
    public sealed class DirectMovement : IMovement
    {
        private readonly DirectMovementDefinition _directMovementDefinition;

        public DirectMovement(DirectMovementDefinition directMovementDefinition)
        {
            _directMovementDefinition = directMovementDefinition;
        }

        public void Move(Transform view, Vector3 position)
        {
            var step = _directMovementDefinition.Speed * Time.deltaTime;
            view.transform.position = Vector3.MoveTowards(view.transform.position, position, step);
        }

        public void Stop()
        { 
        }
    }
}