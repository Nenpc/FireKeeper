using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameLib.Window
{
    public interface IWindowView
    {
        GameObject GameObject { get; }
        bool IsVisible { get; }
        
        UniTask ShowAsync();
        UniTask HideAsync();
    }
}