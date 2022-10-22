using UnityEngine;
using UnityEngine.UI;

namespace TownRally
{
    internal class PanelStartScreen : APanel
    {
        [SerializeField] private Button btnInfo = null;
        [SerializeField] private Button btnMap = null;
        [SerializeField] private Button btnStartRally = null;

        internal override void Init(PanelsHandler.PanelType panelType)
        {
            base.Init(panelType);
            this.btnInfo.onClick.AddListener(OnBtnInfo);
            this.btnMap.onClick.AddListener(OnBtnMap);
            this.btnStartRally.onClick.AddListener(OnBtnStartRally);
        }

        private void OnBtnInfo()
        {
            PanelsHandler.EventIn_SetPanel.Invoke(PanelsHandler.PanelType.RallyInfo);
        }

        private void OnBtnMap()
        {
            PanelsHandler.EventIn_SetPanel.Invoke(PanelsHandler.PanelType.RallyMap);
        }

        private void OnBtnStartRally()
        {
            PanelsHandler.EventIn_SetPanel.Invoke(PanelsHandler.PanelType.StartScreen);
        }
    }
}
