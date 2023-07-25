using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public interface IBonfireFactory
    {
        UniTask<BonfireView> CreateBonfireAsync(Vector3 position);
        void Hide();
    }
}