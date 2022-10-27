using TMPro;
using UnityEngine;

namespace TownRally
{
    internal class UIElementInfoText : AUIElement
    {
        internal override EType ElementType => AUIElement.EType.Text;

        private TextMeshProUGUI tmpText = null;

        internal override void Init(int elementIndex, string elementData)
        {
            base.Init(elementIndex, elementData);
            this.tmpText = this.GetComponent<TextMeshProUGUI>();
            tmpText.text = elementData;
        }
    }
}
