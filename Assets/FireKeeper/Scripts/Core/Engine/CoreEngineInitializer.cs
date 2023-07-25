using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Zenject;

namespace FireKeeper.Core.Engine
{
    public sealed class CoreEngineInitializer : IInitializable
    {
        private const string FirstLevel = "FirstLevel";
        
        private readonly IMissionPreparer _missionPreparer;
        private readonly ICoreTimeController _coreTimeController;
        private readonly List<ICoreExtraInitializer> _coreExtraInitializers;

        public CoreEngineInitializer(IMissionPreparer missionPreparer,
            ICoreTimeController coreTimeController,
            ICoreExtraInitializer[] coreExtraInitializers)
        {
            _missionPreparer = missionPreparer;
            _coreTimeController = coreTimeController;
            _coreExtraInitializers = coreExtraInitializers.OrderByDescending(x => x.Order).ToList();
        }

        public void Initialize()
        {
            RunAsync().Forget();
        }

        private async UniTask RunAsync()
        {
            await _missionPreparer.PrepareMission(FirstLevel);
            
            _coreTimeController.Start();
            
            await ExtraInitializeAsync();
        }

        private async UniTask ExtraInitializeAsync()
        {
            while (_coreExtraInitializers.Count > 0)
            {
                await UniTask.WhenAll(GetNextOrderExtraInitializes());
            }
        }

        private IEnumerable<UniTask> GetNextOrderExtraInitializes()
        {
            for (var i = _coreExtraInitializers.Count - 1; i >= 0; i--)
            {
                var coreExtraInitializer = _coreExtraInitializers[i];
                
                _coreExtraInitializers.RemoveAt(i);
                
                yield return coreExtraInitializer.InitializeAsync();

                var nextIx = i - 1;
                if (nextIx < 0) yield break;
                if (coreExtraInitializer.Order != _coreExtraInitializers[nextIx].Order) yield break;
            }
        }
    }
}