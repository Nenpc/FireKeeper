namespace FireKeeper.Core.Engine
{
    public sealed class TakeFuelState : IInteractionState
    {
        private IPlayerInteractionController _playerInteractionController;
        
        public TakeFuelState(IPlayerInteractionController playerInteractionController)
        {
            _playerInteractionController = playerInteractionController;
        }
        
        public bool Interact(InteractionMono interactionMono)
        {
            if (_playerInteractionController.HasObject()) return false;

            _playerInteractionController.PickedUpObject = interactionMono;
            
            return true;
        }
    }
}