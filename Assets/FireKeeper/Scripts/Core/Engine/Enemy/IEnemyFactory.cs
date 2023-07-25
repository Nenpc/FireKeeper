using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public interface IEnemyFactory
    {
        UniTask<EnemyView> CreateRandomEnemyAsync(Vector3 position);
        UniTask<EnemyView> CreateEnemyBuIDAsync(string id, Vector3 position);
        void DestroyEnemy(EnemyController enemyController);
    }
}