using UnityEngine;

namespace FireKeeper.Config
{
    [CreateAssetMenu(menuName = "Config/Movement/" + nameof(DirectMovementDefinition), fileName = nameof(DirectMovementDefinition))]
    public sealed class DirectMovementDefinition  : MovementDefinitionAbstract
    {
        public override IMovement GetMovement()
        {
            return new DirectMovement(this);
        }
    }
}