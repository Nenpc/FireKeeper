using System;
using FireKeeper.Config;

namespace FireKeeper.Core.Engine
{
    public interface IBonfireController
    {
        event Action GoOutAction;
        void AddLog(float quality);
        IBonfireDefinition Definition { get; }
        void UpdateView(BonfireView view);
    }
}