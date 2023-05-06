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
        private string rallyId = string.Empty;
        private Rally rally;

        internal void Init(string rallyId, Rally rally)
        {
            this.rallyId = rallyId;
            this.rally = rally;
            this.tmpText.text = TXT_LINERALLY.Replace("&1", this.rally.Name).Replace("&2", this.rally.Caption);
            this.btnSelf.onClick.AddListener(OnClick);
        }

        private async void OnClick()
        {
            RalliesHandler.EventIn_SetCurrentRally.Invoke(this.rallyId);
            Debug.Log("GOT IT!!!3");
            PanelsHandler.EventIn_SetPanel.Invoke(PanelsHandler.PanelType.StartScreen);
        }
    }
}