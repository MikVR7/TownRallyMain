using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TownRally
{
    internal class PanelRallyInfo : APanel
    {
        [SerializeField] private RectTransform rtElementsParent = null;
        [SerializeField] private Dictionary<Rally.DescriptionType, GameObject> prefabsUIElements = new Dictionary<Rally.DescriptionType, GameObject>();
        private List<AUIElement> infoElements = new List<AUIElement>();
        [SerializeField] private ScrollviewContent scrollviewContent = null;

        internal override void Init(PanelsHandler.PanelType panelType)
        {
            base.Init(panelType);
            //RalliesHandler.EventOut_RallyChanged.AddListenerSingle(UpdateRallyInfo);
            this.scrollviewContent.Init();
            Settings.EventOut_ValueChanged[Settings.Value.Rally].AddListenerSingle(OnRallyChanged);
            //Settings.EventOut_SettingsChangedStr[Settings.ValStr.RallyID].AddListenerSingle(OnRallyChanged);
        }

        //private void UpdateRallyInfo(Rally rally)
        private void OnRallyChanged()
        {
            Rally rally = RalliesHandler.VarOut_CurrentRally();
            if(rally.Name.Equals(RalliesHandler.NO_RALLY)) { return; }

            // delete old elements
            this.infoElements.ForEach(i => i.DestroyElement());
            this.infoElements.Clear();

            // create new Elements
            Rally.Description[] rallyDescription = rally.Descr;
            for (int i = 0; i < rallyDescription.Length; i++) { this.CreateNewInfoElement(i, rallyDescription[i]); }
        }

        private void CreateNewInfoElement(int index, Rally.Description rallyDescription)
        {
            Type elementType = AUIElement.UIELEMENT_TYPE[rallyDescription.Type];
            GameObject goUIElement = Instantiate(this.prefabsUIElements[rallyDescription.Type], this.rtElementsParent);
            AUIElement uiElement = (goUIElement.GetComponent(elementType) as AUIElement);
            uiElement.Init(index, rallyDescription.Type, rallyDescription.Data);
            this.infoElements.Add(uiElement);
        }
    }
}
