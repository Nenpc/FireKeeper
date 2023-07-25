using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameLib.Window
{
    public abstract class WindowView : MonoBehaviour, IWindowView
    {
        [SerializeField] private SpawnAnimationView[] _spawnAnimationViews;
        
        public GameObject GameObject => gameObject;
        public bool IsVisible => gameObject.activeSelf;
        
        private CancellationToken Token
        {
            get
            {
                if (_token == default)
                {
                    _token = this.GetCancellationTokenOnDestroy();
                }

                return _token;
            }
        }
        
        private CancellationToken _token = default;

        public virtual async UniTask ShowAsync()
        {
            if (_spawnAnimationViews.Length <= 0) return;

            var uniTasks = new UniTask[_spawnAnimationViews.Length];
            for (var i = 0; i < _spawnAnimationViews.Length; i++)
            {
                uniTasks[i] = _spawnAnimationViews[i].ForwardAsync(Token);
            }

            await UniTask.WhenAll(uniTasks);
        }

        public virtual async UniTask HideAsync()
        {
            if (_spawnAnimationViews.Length <= 0) return;
            
            var uniTasks = new UniTask[_spawnAnimationViews.Length];
            for (var i = 0; i < _spawnAnimationViews.Length; i++)
            {
                uniTasks[i] = _spawnAnimationViews[i].BackwardAsync(Token);
            }
            
            await UniTask.WhenAll(uniTasks);
        }
       
    }
}