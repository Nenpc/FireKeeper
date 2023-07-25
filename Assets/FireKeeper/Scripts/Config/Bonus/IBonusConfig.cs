using System.Collections.Generic;

namespace FireKeeper.Config
{
    public interface IBonusConfig
    {
        IReadOnlyList<IBonusDefinition> Definitions { get; }
        IBonusDefinition GetDefinition(string id);
        IBonusDefinition GetRandomDefinition();

        bool HasDefinition(string id);
    }
}