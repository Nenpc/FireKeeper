using System;
using FireKeeper.Config;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public interface IPlayerController
    {
        event Action<EffectTimer> AddEffectAction;
        event Action<EffectTimer> TimeUpdateEffectAction;
        event Action<EffectTimer> RemoveEffectAction;
        
        Vector3 Position { get; }
        void UpdateView(PlayerView view);
        void ApplyEffect(IEffect effect);
    }
}