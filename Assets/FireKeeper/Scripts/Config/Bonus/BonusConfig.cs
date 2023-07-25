using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FireKeeper.Config
{
    [CreateAssetMenu(menuName = "Config/" + nameof(BonusConfig), fileName = nameof(BonusConfig))]
    public sealed class BonusConfig : ScriptableObject, IBonusConfig
    {
        public IReadOnlyList<IBonusDefinition> Definitions => _definitions;

        [SerializeField] private BonusDefinition[] _definitions;

        public IBonusDefinition GetDefinition(string id)
        {
            var definition = Definitions.FirstOrDefault( def => def.Id == id);
            if (definition == default)
            {
                Debug.LogError($"Can't find {nameof(IBonusDefinition)} with id:{id}");
            }

            return definition;
        }

        public IBonusDefinition GetRandomDefinition()
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