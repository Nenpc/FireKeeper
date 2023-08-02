using System;
using FireKeeper.Config;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public interface IPlayerController
    {
        event Action<Collider> FindedObjectAction;
        event Action<Collider> LosedObjectAction;
        
        event Action<EffectTimer> AddEffectAction;
        event Action<EffectTimer> TimeUpdateEffectAction;
        event Action<EffectTimer> RemoveEffectAction;
        
        Vector3 Position { get; }
        Transform FuelPosition { get; }
        void UpdateView(PlayerView view);
        void ApplyEffect(IEffect effect);
    }
}