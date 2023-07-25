using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FireKeeper.Config
{
    [CreateAssetMenu(menuName = "Config/" + nameof(EnemyConfig), fileName = nameof(EnemyConfig))]
    public sealed class EnemyConfig : ScriptableObject, IEnemyConfig
    {
        public IReadOnlyList<IEnemyDefinition> Definitions => _definitions;

        [SerializeField] private EnemyDefinition[] _definitions;

        public IEnemyDefinition GetDefinition(string id)
        {
            var definition = Definitions.FirstOrDefault( def => def.Id == id);
            if (definition == default)
            {
                Debug.LogError($"Can't find {nameof(IEnemyDefinition)} with id:{id}");
            }

            return definition;
        }

        public IEnemyDefinition GetRandomDefinition()
        {
            return _definitions[Random.Range(0, _definitions.Length)];
        }

        public bool HasDefinition(string id)
        {
            var result = Definitions.FirstOrDefault(def => def.Id == id);
            return result != default;
        }
    }
}