using UnityEngine;

namespace FireKeeper.Config
{
    public sealed class JumpMovement : IMovement
    {
        private readonly JumpMovementDefinition _jumpMovementDefinition;

        public JumpMovement(JumpMovementDefinition jumpMovementDefinition)
        {
            _jumpMovementDefinition = jumpMovementDefinition;
        }

        public void Move(Transform view, Vector3 position)
        {
            var step = _jumpMovementDefinition.Speed * Time.deltaTime;
            view.transform.position = Vector3.MoveTowards(view.transform.position, position, step);
        }

        public void Stop()
        {
        }
    }
}