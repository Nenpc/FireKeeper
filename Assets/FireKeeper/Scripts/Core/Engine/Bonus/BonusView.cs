using FireKeeper.Config;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public sealed class BonusView : MonoBehaviour
    {
        public IBonusDefinition BonusDefinition { get; private set; }

        public void Initialize(IBonusDefinition bonusDefinition)
        {
            BonusDefinition = bonusDefinition;
        }
    }
}