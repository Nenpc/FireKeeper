using System.Collections.Generic;
using GameView;
using Managers;
using UnityEngine;
using Zenject;
using Improvement = GameLogic.Improvement;

namespace GameUI
{
    public class ImprovementPanel : MonoBehaviour
    {
        [SerializeField] private ImprovementSettings improvementSettings;

        private List<ImprovementIcon> improvementIcons;

        private IPlayer player;
        private ITimeService timeService;

        [Inject]
        private void Construct(IPlayer player, ITimeService timeService)
        {
            this.player = player;
            this.timeService = timeService;
            
            timeService.TickingSubscribe(TimeTick);
            player.TakeImprovementSubscribe(AddImprovement);
            improvementIcons = new List<ImprovementIcon>();
        }

        private void OnDestroy()
        {
            timeService.TickingUnsubscribe(TimeTick);
            player.TakeImprovementUnsubscribe(AddImprovement);
            improvementIcons = null;
        }

        private void AddImprovement(Improvement improvement)
        {
            ImprovementIcon improvementIcon = null;
            foreach (var improvementSetting in improvementSettings.improvements)
            {
                if (improvementSetting.type == improvement.improvementType)
                {
                    improvementIcon = Instantiate(improvementSetting.Icon, transform);
                    improvementIcons.Add(improvementIcon);
                    improvementIcon.StartTime(improvement);
                    return;
                }
            }
        }

        private void TimeTick(int time)
        {
            for (int i = 0; i < improvementIcons.Count; i++)
            {
                if (!improvementIcons[i].TimeEnd())
                {
                    improvementIcons[i].UpdateTimeProgress();
                }
                else
                {
                    var temp = improvementIcons[i].gameObject;
                    improvementIcons.Remove(improvementIcons[i]);
                    Destroy(temp);
                    i--;
                }
            }
        }
    }
}