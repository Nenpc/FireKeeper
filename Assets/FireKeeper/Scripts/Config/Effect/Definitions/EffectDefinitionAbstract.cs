using UnityEngine;

namespace FireKeeper.Config
{
    public abstract class EffectDefinitionAbstract : ScriptableObject
    {
        [SerializeField] private string _iconKey;
        [SerializeField] private float _time;
        [SerializeField] private bool _infinityTime;
        [SerializeField] private bool _isGoodEffect;
        
        public float Time => _time;
        public bool InfinityTime => _infinityTime;
        public string IconKey => _iconKey;
        public bool IsGoodEffect => _isGoodEffect;
        public abstract IEffect GetEffect();
    }
}