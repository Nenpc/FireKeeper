using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FireKeeper.Config
{
    public interface IMissionDefinition
    {
        string Id { get; }
        AssetReferenceGameObject MapView { get; }
        int StartFuel { get; }
        int StartBonus { get; }
        int StartEnemy { get; }
        int MaxFuel { get; }
        int MaxBonus { get; }
        int MaxEnemy { get; }
        int MaxBonfire { get; }

        int WinTime { get; }
        Vector2 Size { get; }
        int Indent { get; }
    }
}