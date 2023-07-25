using System.Collections.Generic;

namespace FireKeeper.Config
{
    public interface IEnemyConfig
    {
        IReadOnlyList<IEnemyDefinition> Definitions { get; }
        IEnemyDefinition GetDefinition(string id);
        IEnemyDefinition GetRandomDefinition();
        bool HasDefinition(string id);
    }
}