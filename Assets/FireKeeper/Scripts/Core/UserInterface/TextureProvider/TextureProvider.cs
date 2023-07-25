using Cysharp.Threading.Tasks;
using FireKeeper.Core.Engine;
using UnityEngine.UI;

namespace FireKeeper.Core.UserInterface
{
    public sealed class TextureProvider : ITextureProvider
    {
        private readonly IAssetPropsLoader _assetPropsLoader;
        
        public TextureProvider(IAssetPropsLoader assetPropsLoader)
        {
            _assetPropsLoader = assetPropsLoader;
        }

        public async UniTask SetIcon(Image image, string atlasKey)
        {
            if (string.IsNullOrEmpty(atlasKey))
                return;
            
            var sprite = await _assetPropsLoader.LoadSpriteAsync(atlasKey);
            image.sprite = sprite;
            image.gameObject.SetActive(sprite != null);  
        }
    }
}