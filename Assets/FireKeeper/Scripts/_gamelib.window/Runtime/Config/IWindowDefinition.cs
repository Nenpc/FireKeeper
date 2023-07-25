using System;
using UnityEngine.AddressableAssets;

namespace GameLib.Window
{
    public interface IWindowDefinition
    {
        string WindowType { get; }
        AssetReferenceGameObject AssetReferencePrefab { get; }
        WindowInitializeState InitializeState { get; }
        WindowHideState HideState { get; }
        int Order { get; }
    }
}