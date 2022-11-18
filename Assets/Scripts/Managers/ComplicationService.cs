using GameView.SO;
using UnityEngine;

namespace Managers
{
    public class ComplicationService : MonoBehaviour
    {
        [SerializeField] private ComplicationsSetting complicationsSetting;

        private TimeService timeService;
        private bool inProgress;

        private void Construct(TimeService timeService)
        {
            this.timeService = timeService;
            this.timeService.TickingEvent += CreateComplication;
        }

        private void OnDestroy()
        {
            timeService.TickingEvent -= CreateComplication;
        }

        private void CreateComplication(int time)
        {
            if (inProgress)
                return;

        }
    }
}