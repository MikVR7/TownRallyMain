using UnityEngine;

namespace TownRally
{
    internal class PanelsHandlerBody : MonoBehaviour
    {
        internal enum PanelType
        {
            Login = 0,
            RallySelection = 1,
            Rally = 2,
        }

        internal static EventIn_OnBtnPanelBack EventIn_OnBtnPanelBack = new EventIn_OnBtnPanelBack();
        internal static EventIn_SetPanelBody EventIn_SetPanelBody = new EventIn_SetPanelBody();

        [SerializeField] private PanelLogin panelLogin = null;
        [SerializeField] private PanelRallySelection panelRallySelection = null;
        [SerializeField] private PanelRally panelRally = null;

        private PanelType currentPanel = PanelType.Login;

        internal void Init()
        {
            this.panelLogin.Init();
            this.panelRallySelection.Init();
            this.panelRally.Init();
            EventIn_OnBtnPanelBack.AddListener(OnBtnPanelBack);
            EventIn_SetPanelBody.AddListener(SetPanelBody);
            SetPanelBody(PanelType.Login);
        }

        private void OnBtnPanelBack()
        {
            if(currentPanel == PanelType.Login) { SetPanelBody(PanelType.Login); }
            else if(currentPanel == PanelType.RallySelection) { SetPanelBody(PanelType.Login); }
            else if (currentPanel == PanelType.Rally) { SetPanelBody(PanelType.RallySelection); }
        }

        private void SetPanelBody(PanelType panelType)
        {
            this.currentPanel = panelType;
            this.panelLogin.gameObject.SetActive(panelType.Equals(PanelType.Login));
            this.panelRallySelection.gameObject.SetActive(panelType.Equals(PanelType.RallySelection));
            this.panelRally.gameObject.SetActive(panelType.Equals(PanelType.Rally));
        }
    }
}
