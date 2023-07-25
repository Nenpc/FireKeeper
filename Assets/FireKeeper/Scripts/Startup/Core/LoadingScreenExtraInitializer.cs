using System;
using Cysharp.Threading.Tasks;
using FireKeeper.Core.Engine;

namespace Core.Startup
{
    public class LoadingScreenExtraInitializer : ICoreExtraInitializer
    {
        public int Order => Int32.MaxValue;
        
        public LoadingScreenExtraInitializer()
        {
        }
        
        public UniTask InitializeAsync()
        {
            return UniTask.CompletedTask;
        }
    }
}