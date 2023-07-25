using Cysharp.Threading.Tasks;
using UnityEngine.UI;

namespace FireKeeper.Core.UserInterface
{
    public interface ITextureProvider
    {
        UniTask SetIcon(Image image, string atlasKey);
    }
}