using GameView.SO;
using UnityEngine;

namespace Managers
{
    interface IComplicationService
    {
        
    }
    
    public class ComplicationService : MonoBehaviour, IComplicationService
    {
        [SerializeField] private ComplicationsSetting complicationsSetting;

        private TimeService timeService;
        private bool inProgress;

        private void Construct(TimeService timeService)
        {
            this.timeService = timeService;
            this.timeService.TickingSubscribe(CreateComplication);
        }

        private void OnDestroy()
        {
            timeService.TickingUnsubscribe(CreateComplication);
        }

        private void CreateComplication(int time)
        {
            if (inProgress)
                return;
            
        }

    }
}