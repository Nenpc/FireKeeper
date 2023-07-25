using Cysharp.Threading.Tasks;

namespace FireKeeper.Core.Engine
{
    public class CoreEngineExtraInitializer : ICoreExtraInitializer
    {
        public int Order => 0;

        public CoreEngineExtraInitializer()
        {
        }

        public UniTask InitializeAsync()
        {
            return UniTask.CompletedTask;
        }
    }
}