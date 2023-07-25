using Cysharp.Threading.Tasks;
using FireKeeper.Core.Engine;

namespace Core.Startup
{
    public sealed class StartupCoreExtraInitializer : ICoreExtraInitializer
    {
        public int Order => 50;

        public StartupCoreExtraInitializer()
        {
        }

        public async UniTask InitializeAsync()
        {

        }
    }
}