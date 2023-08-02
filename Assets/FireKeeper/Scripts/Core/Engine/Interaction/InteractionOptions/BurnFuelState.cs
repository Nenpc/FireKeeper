using FireKeeper.Config;

namespace FireKeeper.Core.Engine
{
    public sealed class BurnFuelState : IInteractionState
    {
        private IPlayerInteractionController _playerInteractionController;
        
        public BurnFuelState(IPlayerInteractionController playerInteractionController)
        {
            _playerInteractionController = playerInteractionController;
        }
        
        public bool Interact(InteractionMono interactionMono)
        {
            var fuelDefinition = GetFuel();
            if (fuelDefinition == null)
                return false;

            var bonfireController = GetBonfireController(interactionMono);
            if (bonfireController == null)
                return false;
            
            bonfireController.AddLog(fuelDefinition.Power);
            DeleteFuel();
            _playerInteractionController.PickedUpObject = null;

            
            return true;
        }

        private IFuelDefinition GetFuel()
        {
            var inventoryObject = _playerInteractionController.PickedUpObject;
            if (inventoryObject == null)
                return null;

            var fuelDefinition = inventoryObject.GetInteractionInfo().InteractObject as IFuelDefinition;
            if (fuelDefinition == null)
                return null;

            return fuelDefinition;
        }
        
        private void DeleteFuel()
        {
            var inventoryObject = _playerInteractionController.PickedUpObject;
            if (inventoryObject == null)
                return;

            inventoryObject.GetInteractionInfo().DeleteObject();
        }
        
        private IBonfireController GetBonfireController(InteractionMono interactionMono)
        {
            var bonfireController = interactionMono.GetInteractionInfo().InteractObject as IBonfireController;
            if (bonfireController == null)
                return null;

            return bonfireController;
        }
        
    }
}