using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameLib.Window
{
    [CreateAssetMenu(menuName = "GameLib/" + nameof(WindowDefinition), fileName = nameof(WindowDefinition))]
    public sealed class WindowDefinition : ScriptableObject, IWindowDefinition
    {
        [SerializeField] private string _windowType;
        [SerializeField] private AssetReferenceGameObject _assetReferencePrefab;
        [SerializeField] private WindowInitializeState _initializeState;
        [SerializeField] private WindowHideState _hideState;
        [SerializeField] private int _order;
        
        public string WindowType => _windowType;
        public AssetReferenceGameObject AssetReferencePrefab => _assetReferencePrefab;
        public WindowInitializeState InitializeState => _initializeState;
        public WindowHideState HideState => _hideState;
        public int Order => _order;
    }
}