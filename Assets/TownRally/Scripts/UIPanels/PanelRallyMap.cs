using UnityEngine;

namespace TownRally
{
    internal class PanelRallyMap : APanel
    {
        internal override void Init(PanelsHandler.PanelType panelType)
        {
            base.Init(panelType);
        }

        private void OnEnable()
        {
            MapsHandler.EventIn_DisplayMap.Invoke(true);
        }

        private void OnDisable()
        {
            MapsHandler.EventIn_DisplayMap.Invoke(false);
        }
    }
}
