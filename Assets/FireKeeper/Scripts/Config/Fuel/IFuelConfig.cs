using System.Collections.Generic;

namespace FireKeeper.Config
{
    public interface IFuelConfig
    {
        IReadOnlyList<IFuelDefinition> Definitions { get; }
        IFuelDefinition GetDefinition(string id);
        IFuelDefinition GetRandomDefinition();
        bool HasDefinition(string id);
    }
}