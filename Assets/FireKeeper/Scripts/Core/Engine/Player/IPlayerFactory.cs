using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public interface IPlayerFactory
    {
        UniTask<PlayerView> CreatePlayerAsync(Vector3 position);
        void Hide();
    }
}