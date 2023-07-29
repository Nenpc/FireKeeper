using UnityEngine;

namespace FireKeeper.Config
{
    [CreateAssetMenu(menuName = "Config/Movement/" + nameof(JumpMovementDefinition), fileName = nameof(JumpMovementDefinition))]
    public sealed class JumpMovementDefinition : MovementDefinitionAbstract
    {
        public override IMovement GetMovement()
        {
            return new JumpMovement(this);
        }
    }
}