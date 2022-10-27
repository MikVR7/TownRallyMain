using System;
using System.Collections.Generic;
using UnityEngine;

namespace TownRally
{
    internal class PanelRallyInfo : APanel
    {
        [SerializeField] private RectTransform rtElementsParent = null;
        [SerializeField] private Dictionary<Type, GameObject> prefabsUIElements = new Dictionary<Type, GameObject>();
        private List<AUIElement> infoElements = new List<AUIElement>();
        [SerializeField] private ScrollviewContent scrollviewContent = null;

        internal override void Init(PanelsHandler.PanelType panelType)
        {
            base.Init(panelType);
            RalliesHandler.EventOut_RallyChanged.AddListenerSingle(UpdateRallyInfo);
            this.scrollviewContent.Init();
        }

        private void UpdateRallyInfo(Rally rally)
        {
            // delete old elements
            this.infoElements.ForEach(i => i.DestroyElement());
            this.infoElements.Clear();

            // create new Elements
            List<string> rallyDescription = rally.Description;
            for(int i = 0; i < rallyDescription.Count; i++) { this.CreateNewInfoElement(i, rallyDescription[i]); }
        }

        private void CreateNewInfoElement(int index, string element)
        {
            string prefix = element.Substring(0, 4);
            string key = prefix.Replace(":", "");
            Type elementType = AUIElement.UIELEMENT_TYPE[key];
            GameObject goUIElement = Instantiate(this.prefabsUIElements[elementType], this.rtElementsParent);
            AUIElement uiElement = (goUIElement.GetComponent(elementType) as AUIElement);
            uiElement.Init(index, element.Replace(prefix, string.Empty));
            this.infoElements.Add(uiElement);
        }
    }
}
