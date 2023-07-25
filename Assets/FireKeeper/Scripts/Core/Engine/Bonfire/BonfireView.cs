using FireKeeper.Config;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public sealed class BonfireView : MonoBehaviour
    {
        public IBonfireDefinition BonfireDefinition { get; private set; }

        public void Initialize(IBonfireDefinition bonfireDefinition)
        {
            BonfireDefinition = bonfireDefinition;
        }
    }
}