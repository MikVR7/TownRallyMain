using UnityEngine;
using UnityEngine.UI;

namespace TownRally
{
    internal class UIElementInfoPicture : AUIElement
    {
        [SerializeField] private RawImage image = null;
        private Texture texture = null;
        private Material material = null;

        internal override void Init(int elementIndex, Rally.DescriptionType descriptionType, string elementData)
        {
            base.Init(elementIndex, descriptionType, elementData);
            this.SetTexture(this.descriptionData);
        }

        private void SetTexture(string path)
        {
            Debug.Log("SET TEXTURE!)");
            this.material = new Material(Shader.Find("UI/Unlit/Detail"));
            //path = path.Replace(".jpg", string.Empty).Replace(".png", string.Empty);
            //Debug.Log("RESOURCES: " + path);
            //this.texture = Resources.Load<Texture>(path);
            //this.image.texture = this.texture;
            this.image.material = this.material;
            DatabaseHandler.EventInOut_LoadImage.Invoke(path, OnLoadedImage);
            Debug.Log("IMAGE SET AS TEXTURE!");
        }

        private void OnLoadedImage(Texture2D texture)
        {
            Debug.Log("DONE LOADING!");
            this.texture = texture;
            this.image.texture = this.texture;
        }
    }
}
