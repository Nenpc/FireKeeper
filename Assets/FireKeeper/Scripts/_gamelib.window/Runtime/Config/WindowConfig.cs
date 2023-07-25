using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GameLib.Window
{
    [CreateAssetMenu(menuName = "GameLib/" + nameof(WindowConfig), fileName = nameof(WindowConfig))]
    public sealed class WindowConfig : ScriptableObject, IWindowConfig
    {
        [SerializeField] private WindowDefinition[] _windowDefinitions;
        
        public IReadOnlyList<IWindowDefinition> Definitions => _windowDefinitions;
        
        public IWindowDefinition GetDefinition<T>()
        {
            var sourceWindowType = typeof(T);
            return GetDefinition(sourceWindowType);
        }

        public IWindowDefinition GetDefinition(Type windowType)
        {
            foreach (var windowDefinition in _windowDefinitions)
            {
                if (windowDefinition.WindowType != windowType.ToString()) continue;

                return windowDefinition;
            }

            Debug.LogError($"Can't find {nameof(WindowDefinition)} for windowType:{windowType}");
            return default;
        }
        
        public IWindowDefinition GetDefinition(string id)
        {
            foreach (var windowDefinition in _windowDefinitions)
            {
                if (windowDefinition.WindowType != id) continue;

                return windowDefinition;
            }

            Debug.LogError($"Can't find {nameof(WindowDefinition)} for id:{id}");
            return default;
        }

        private void OnValidate()
        {
            for (int i = 0; i < _windowDefinitions.Length - 1; i++)
            {
                for (int j = i + 1; j < _windowDefinitions.Length; j++)
                {
                    if (_windowDefinitions[i] == _windowDefinitions[j])
                    {
                        Debug.LogError("Parameters 1 and 2 have the same id!");
                    }
                }
            }
        }
    }
}