using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TownRally
{
    internal class PanelRallySelectionLine : MonoBehaviour
    {
        private static string TXT_LINERALLY = "&1<size=70%><br>&2";

        [SerializeField] private Button btnSelf = null;
        [SerializeField] private TextMeshProUGUI tmpText = null;
        private int id = 0;
        private string name = string.Empty;
        private string caption = string.Empty;

        internal void Init(int id, string name, string caption)
        {
            this.id = id;
            this.name = name;
            this.caption = caption;
            this.tmpText.text = TXT_LINERALLY.Replace("&1", this.name).Replace("&2", this.caption);
            this.btnSelf.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            RalliesHandler.EventIn_SetCurrentRally.Invoke(this.id);
            TaskBarHandler.EventIn_SetTaskBarRallyname.Invoke(this.name);
            PanelsHandler.EventIn_SetPanelBody.Invoke(PanelsHandler.PanelType.Rally);
        }
    }
}
