using UnityEngine;

namespace TownRally
{
    internal abstract class APanel : MonoBehaviour
    {
        public PanelsHandler.PanelType VarOut_PanelType { get; protected set; } = PanelsHandler.PanelType.None;
        
        internal virtual void Init(PanelsHandler.PanelType panelType)
        {
            this.VarOut_PanelType = panelType;
        }
        
        internal void SetActive(bool active)
        {
            this.gameObject.SetActive(active);
        }
    }
}
