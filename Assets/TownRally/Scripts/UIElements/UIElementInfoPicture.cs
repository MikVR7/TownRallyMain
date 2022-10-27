using UnityEngine;
using UnityEngine.UI;

namespace TownRally
{
    internal class UIElementInfoPicture : AUIElement
    {
        internal override EType ElementType => AUIElement.EType.Picture;

        private RawImage image = null;
        private Texture texture = null;

        internal override void Init(string elementData)
        {
            base.Init(elementData);
            this.image = this.GetComponent<RawImage>();
            this.SetTexture(this.elementData);
        }

        private void SetTexture(string path)
        {
            path = path.Replace(".jpg", string.Empty).Replace(".png", string.Empty);
            this.texture = Resources.Load<Texture>(path);
            this.image.texture = this.texture;
        }
    }
}
