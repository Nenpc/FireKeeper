namespace FireKeeper.Core.Engine
{
    public interface IPlayerInteractionController
    {
        InteractionMono PickedUpObject { get; set; }
        bool HasObject();
        void Interact();
    }
}