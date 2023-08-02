namespace FireKeeper.Core.Engine
{
    public sealed class InteractionState : IInteractionState
    {
        private IPlayerInteractionController _playerInteractionController;
        
        public InteractionState(IPlayerInteractionController playerInteractionController)
        {
            _playerInteractionController = playerInteractionController;
        }

        public bool Interact(InteractionMono interactionMono)
        {
            return true;
        }
    }
}