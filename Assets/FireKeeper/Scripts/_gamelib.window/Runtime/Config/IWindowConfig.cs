using System;
using System.Collections.Generic;

namespace GameLib.Window
{
    public interface IWindowConfig
    {
        IReadOnlyList<IWindowDefinition> Definitions { get; }
        IWindowDefinition GetDefinition<T>();
        IWindowDefinition GetDefinition(Type windowType);
    }
}