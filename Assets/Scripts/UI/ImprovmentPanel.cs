using System;
using System.Collections;
using System.Collections.Generic;
using GameLogic;
using GameView;
using Managers;
using UnityEngine;
using Zenject;

namespace GameUI
{
    public class ImprovmentPanel : MonoBehaviour
    {
        [SerializeField] private ImprovementSettings improvementSettings;

        private List<ImprovementIcon> improvementIcons;

        private Player player;
        private TimeManager timeManager;

        [Inject]
        private void Construct(Player player, TimeManager timeManager)
        {
            this.player = player;
            this.timeManager = timeManager;
            
            timeManager.Tiking += TimeTick;
            player.takeImprovment += AddImprovement;
            improvementIcons = new List<ImprovementIcon>();
        }

        private void OnDestroy()
        {
            timeManager.Tiking -= TimeTick;
            player.takeImprovment -= AddImprovement;
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