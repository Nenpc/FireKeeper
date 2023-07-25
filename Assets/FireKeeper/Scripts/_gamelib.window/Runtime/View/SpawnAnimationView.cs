using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameLib.Window
{
    public abstract class SpawnAnimationView : MonoBehaviour
    {
        public abstract UniTask ForwardAsync(CancellationToken token);
        public abstract UniTask BackwardAsync(CancellationToken token);
    }
}