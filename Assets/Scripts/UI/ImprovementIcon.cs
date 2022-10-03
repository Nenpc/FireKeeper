using System.Collections;
using GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{

    public class ImprovementIcon : MonoBehaviour
    {
        [SerializeField] private Image progressImage; 
        private Improvement improvement;
        private float maxTime;
        
        public void StartTime(Improvement improvement)
        {
            maxTime = improvement.Time;
            this.improvement = improvement;
            progressImage.rectTransform.localScale = new Vector3(1, 1, 1);
        }

        public void UpdateTimeProgress()
        {
            progressImage.rectTransform.localScale = new Vector3(1, improvement.Time / maxTime, 1);
        }

        public bool TimeEnd()
        {
            return improvement.Time == 0;
        }
    }
}