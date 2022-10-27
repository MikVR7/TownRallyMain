using TMPro;
using UnityEngine;

namespace TownRally
{
    internal class UIElementInfoText : AUIElement
    {
        internal override EType ElementType => AUIElement.EType.Text;

        private TextMeshProUGUI tmpText = null;

        internal override void Init(string elementData)
        {
            base.Init(elementData);
            this.tmpText = this.GetComponent<TextMeshProUGUI>();
            tmpText.text = elementData;
        }
    }
}
