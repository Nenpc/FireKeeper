using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public interface IBonusFactory
    {
        public void Dispose();
        public UniTask<BonusView> CreateRandomBonusAsync(Vector3 position);
        public UniTask<BonusView> CreateBonusBuIDAsync(string id, Vector3 position);
        public void Destroy(BonusView bonusView);
    }
}