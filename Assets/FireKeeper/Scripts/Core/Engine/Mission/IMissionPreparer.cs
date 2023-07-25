using Cysharp.Threading.Tasks;

namespace FireKeeper.Core.Engine
{
    public interface IMissionPreparer
    {
        UniTask PrepareMission(string id);
    }
}