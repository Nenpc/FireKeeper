using System;
using System.Collections.Generic;
using GameLogic;
using UnityEngine;

namespace GameView.SO
{
    [System.Serializable]
    public class ComplicationSetting
    {
        public ComplicationType complicationType;
        public int duration;
        [Range(0.001f, 30f)]public float probability;
    }
    
    [CreateAssetMenu(fileName = "ComplicationsSetting", menuName = "GameSettings/ComplicationsSetting", order = 13)]
    public class ComplicationsSetting : ScriptableObject
    {
        [SerializeField] public List<ComplicationSetting> ComplicationSettings;
        private void OnValidate()
        {
            if (ComplicationSettings.Count != Enum.GetNames(typeof(ComplicationType)).Length)
            {
                Debug.Log("<color=red>Attention! The number of complications in the list does not match the number of complications in enum.</color>");
            }
        }
    }
    
}