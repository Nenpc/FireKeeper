using System.Collections.Generic;
using UnityEngine;

namespace FireKeeper.Config
{
    public interface IMissionConfig
    {
        IReadOnlyList<IMissionDefinition> Definitions { get; }
        
        IMissionDefinition GetDefinition(string id);
        IMissionDefinition GetFirstDefinition();
        IMissionDefinition GetNextDefinition(IMissionDefinition missionDefinition);
        Vector3 GetRandomPosition(string id);

        bool HasDefinition(string id);
    }
}