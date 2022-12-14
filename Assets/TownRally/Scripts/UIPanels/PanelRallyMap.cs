using AndroidServices;
using UnityEngine;
using UnityEngine.UI;

namespace TownRally
{
    internal class PanelRallyMap : APanel
    {
        [SerializeField] private Button btnGotoMap = null;
        [SerializeField] private Button btnGotoMypos = null;

        internal override void Init(PanelsHandler.PanelType panelType)
        {
            base.Init(panelType);
            this.btnGotoMap.onClick.AddListener(OnBtnGotoMap);
            this.btnGotoMypos.onClick.AddListener(OnBtnGotoMypos);
        }

        private void OnEnable()
        {
            MapsHandler.EventIn_DisplayMap.Invoke(true);
        }

        private void OnDisable()
        {
            MapsHandler.EventIn_DisplayMap.Invoke(false);
        }

        private void OnBtnGotoMap()
        {
            MapsHandler.EventIn_SetMapPosition.Invoke(new AndroidBridge.Location() { latitude = 47.073863f, longitude = 15.440505f });
        }

        private void OnBtnGotoMypos()
        {
            MapsHandler.EventIn_SetMapCurrentGPSPosition.Invoke();
        }
    }
}
