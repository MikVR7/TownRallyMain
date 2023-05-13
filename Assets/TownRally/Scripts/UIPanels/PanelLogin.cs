using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TownRally
{
    internal class PanelLogin : APanel
    {
        private static PanelLogin Instance = null;
        internal static EventOut_UsernameChanged EventOut_UsernameChanged = new EventOut_UsernameChanged();

        [SerializeField] private Button btnContinue = null;
        [SerializeField] private TMP_InputField inputName = null;
        
        internal override void Init(PanelsHandler.PanelType panelType)
        {
            base.Init(panelType);
            Instance = this;
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
            EventOut_UsernameChanged.Invoke(this.inputName.text);
            PanelsHandler.EventIn_SetPanel.Invoke(PanelsHandler.PanelType.RallySelection);
        }
    }
}
