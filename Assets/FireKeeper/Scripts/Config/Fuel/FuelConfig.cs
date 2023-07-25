using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FireKeeper.Config
{
    [CreateAssetMenu(menuName = "Config/" + nameof(FuelConfig), fileName = nameof(FuelConfig))]
    public sealed class FuelConfig : ScriptableObject, IFuelConfig
    {
        public IReadOnlyList<IFuelDefinition> Definitions => _definitions;

        [SerializeField] private FuelDefinition[] _definitions;

        public IFuelDefinition GetDefinition(string id)
        {
            var definition = Definitions.FirstOrDefault( def => def.Id == id);
            if (definition == default)
            {
                Debug.LogError($"Can't find {nameof(IFuelDefinition)} with id:{id}");
            }

            return definition;
        }

        public IFuelDefinition GetRandomDefinition()
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