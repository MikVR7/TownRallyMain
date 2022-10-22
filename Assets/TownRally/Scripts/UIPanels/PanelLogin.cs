using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TownRally
{
    internal class PanelLogin : APanel
    {
        [SerializeField] private Button btnContinue = null;
        [SerializeField] private TMP_InputField inputName = null;
        
        private string username = string.Empty;

        internal override void Init(PanelsHandler.PanelType panelType)
        {
            base.Init(panelType);
            this.btnContinue.onClick.AddListener(OnBtnContinue);
            this.btnContinue.interactable = false;
            this.inputName.onValueChanged.AddListener(OnValidateInputName);
        }

        private void OnValidateInputName(string value)
        {
            this.btnContinue.interactable = this.inputName.text.Length > 2;
        }

        private void OnBtnContinue()
        {
            this.username = this.inputName.text;
            PanelsHandler.EventIn_SetPanel.Invoke(PanelsHandler.PanelType.RallySelection);
        }
    }
}
