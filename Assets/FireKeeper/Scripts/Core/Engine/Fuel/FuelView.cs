using FireKeeper.Config;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public sealed class FuelView : MonoBehaviour
    {
        public IFuelDefinition FuelDefinition { get; private set; }

        public void Initialize(IFuelDefinition fuelDefinition)
        {
            FuelDefinition = fuelDefinition;
        }
    }
}