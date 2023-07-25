using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FireKeeper.Config
{
    [CreateAssetMenu(menuName = "Config/" + nameof(MissionConfig), fileName = nameof(MissionConfig))]
    public sealed class MissionConfig : ScriptableObject, IMissionConfig
    {
        public IReadOnlyList<IMissionDefinition> Definitions => _definitions;

        [SerializeField] private MissionDefinition[] _definitions;

        public IMissionDefinition GetDefinition(string id)
        {
            var definition = Definitions.FirstOrDefault( def => def.Id == id);
            if (definition == default)
            {
                Debug.LogError($"Can't find {nameof(IMissionDefinition)} with id:{id}");
            }

            return definition;
        }

        public IMissionDefinition GetFirstDefinition()
        {
            return Definitions[0];
        }

        public IMissionDefinition GetNextDefinition(IMissionDefinition missionDefinition)
        {
            for (var i = 0; i < Definitions.Count; i++)
            {
                var currentDefinition = Definitions[i];
                if (!currentDefinition.Equals(missionDefinition)) continue;
            
                var nextIx = i + 1;
                if (nextIx >= Definitions.Count) nextIx = 0;
            
                return Definitions[nextIx];
            }

            Debug.LogError($"No definition with this {nameof(IMissionDefinition)}:{missionDefinition.Id}");
            return default;
        }

        public Vector3 GetRandomPosition(string id)
        {
            var definition = GetDefinition(id);
            var randomX = Random.Range(0, definition.Size.x - definition.Indent * 2);
            var randomZ = Random.Range(0, definition.Size.y - definition.Indent * 2);
            return new Vector3(randomX, 0, randomZ);
        }

        public bool HasDefinition(string id)
        {
            return GetDefinition(id) != default;
        }
    }
}