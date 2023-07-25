using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public interface IFuelFactory
    {
        void Dispose();
        UniTask<FuelView> CreateRandomFuelAsync(Vector3 position);
        UniTask<FuelView> CreateFuelBuIDAsync(string id, Vector3 position);
        void Destroy(FuelView bonusView);
    }
}