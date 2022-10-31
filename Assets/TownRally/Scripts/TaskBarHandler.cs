using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TownRally
{
    internal class TaskBarHandler : MonoBehaviour
    {
        internal static EventIn_SetActiveBtnBack EventIn_SetActiveBtnBack = new EventIn_SetActiveBtnBack();
        internal static EventIn_SetTaskBarUsername EventIn_SetTaskBarUsername = new EventIn_SetTaskBarUsername();
        internal static EventIn_SetTaskBarRallyname EventIn_SetTaskBarRallyname = new EventIn_SetTaskBarRallyname();

        [SerializeField] private Button btnBack = null;
        [SerializeField] private TextMeshProUGUI tmpUsername = null;
        [SerializeField] private TextMeshProUGUI tmpRallyname = null;

        internal void Init()
        {
            this.btnBack.onClick.AddListener(OnBtnBack);
            EventIn_SetTaskBarUsername.AddListenerSingle(SetTaskBarUsername);
            EventIn_SetTaskBarRallyname.AddListenerSingle(SetTaskBarRallyname);
            EventIn_SetActiveBtnBack.AddListenerSingle(SetActiveBtnBack);
            this.tmpUsername.text = string.Empty;
            this.tmpRallyname.text = string.Empty;
        }

        private void OnBtnBack()
        {
            PanelsHandler.EventIn_OnBtnPanelBack.Invoke();
        }

        private void SetActiveBtnBack(bool active)
        {
            this.btnBack.gameObject.SetActive(active);
        }

        private void SetTaskBarUsername(string username)
        {
            this.tmpUsername.text = username;
        }

        private void SetTaskBarRallyname(string rallyname)
        {
            this.tmpRallyname.text = rallyname;
        }
    }
}
