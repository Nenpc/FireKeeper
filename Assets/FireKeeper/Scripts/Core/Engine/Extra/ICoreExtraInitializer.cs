using Cysharp.Threading.Tasks;

namespace FireKeeper.Core.Engine
{
    public interface ICoreExtraInitializer
    {
        int Order { get; }
        UniTask InitializeAsync();
    }
}