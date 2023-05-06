using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TownRally
{
    internal class TaskBarHandler : MonoBehaviour
    {
        internal static EventOut_OnBtnDebug EventOut_OnBtnDebug = new EventOut_OnBtnDebug();

        [SerializeField] private Button btnBack = null;
        [SerializeField] private Button btnDebug = null;
        [SerializeField] private TextMeshProUGUI tmpUsername = null;
        [SerializeField] private TextMeshProUGUI tmpRallyname = null;
        [SerializeField] private Image imgDebugActive = null;

        internal void Init()
        {
            this.btnBack.onClick.AddListener(OnBtnBack);
            this.btnDebug.onClick.AddListener(OnBtnDebug);
            this.tmpUsername.text = string.Empty;
            this.tmpRallyname.text = string.Empty;
            Settings.EventOut_ValueChanged[Settings.Value.Username].AddListenerSingle(OnUsernameChanged);
            Settings.EventOut_ValueChanged[Settings.Value.Rally].AddListenerSingle(OnSelectedRallyChanged);
            Settings.EventOut_ValueChanged[Settings.Value.CurrentPanel].AddListenerSingle(OnCurrentPanelChanged);
        }

        private void OnUsernameChanged()
        {
            this.tmpUsername.text = PanelLogin.VarOut_GetUsername();
        }

        private void OnSelectedRallyChanged()
        {
            this.tmpRallyname.text = RalliesHandler.VarOut_CurrentRally().Name;
        }

        private void OnCurrentPanelChanged()
        {
            PanelsHandler.PanelType type = PanelsHandler.VarOut_CurrentPanel;
            this.btnBack.gameObject.SetActive(
                !type.Equals(PanelsHandler.PanelType.Loading) &&
                !type.Equals(PanelsHandler.PanelType.Login) &&
                !type.Equals(PanelsHandler.PanelType.Rally) &&
                !type.Equals(PanelsHandler.PanelType.None));
        }

        private void OnBtnBack()
        {
            PanelsHandler.EventIn_OnBtnPanelBack.Invoke();
        }

        private void OnBtnDebug()
        {
            //Settings.IsDebugModeOn = !Settings.IsDebugModeOn;
            //this.imgDebugActive.gameObject.SetActive(Settings.IsDebugModeOn);
            //EventOut_OnBtnDebug.Invoke();
        }
    }
}
