using UnityEngine;

namespace FireKeeper.Config
{
    public abstract class EffectDefinitionAbstract : ScriptableObject
    {
        [SerializeField] private string _iconKey;
        [SerializeField] private float _time;
        [SerializeField] private bool _infinityTime;
        
        public float Time => _time;
        public bool InfinityTime => _infinityTime;
        public string IconKey => _iconKey;
        public abstract IEffect GetEffect();
    }
}