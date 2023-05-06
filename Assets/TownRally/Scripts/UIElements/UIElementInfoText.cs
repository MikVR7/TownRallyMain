using TMPro;
using UnityEngine;

namespace TownRally
{
    internal class UIElementInfoText : AUIElement
    {
        //internal override EType ElementType => AUIElement.EType.Text;

        private TextMeshProUGUI tmpText = null;

        //internal override void Init(int elementIndex, string elementData)
        internal override void Init(int elementIndex, Rally.DescriptionType descriptionType, string elementData)
        {
            base.Init(elementIndex, descriptionType, elementData);
            this.tmpText = this.GetComponent<TextMeshProUGUI>();
            tmpText.text = elementData;
        }
    }
}
