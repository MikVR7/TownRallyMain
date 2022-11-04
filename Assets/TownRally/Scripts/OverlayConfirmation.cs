using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TownRally
{
    internal class OverlayConfirmation : MonoBehaviour
    {
        internal static EventIn_DisplayOverlayConfirmation EventIn_DisplayOverlayConfirmation = new EventIn_DisplayOverlayConfirmation();

        [SerializeField] private TextMeshProUGUI tmpInfo = null;
        [SerializeField] private Button btnRallyEnd = null;
        [SerializeField] private Button btnRallyPause = null;
        [SerializeField] private Button btnRallyContinue = null;
        [SerializeField] private Button btnClose = null;

        internal void Init()
        {
            EventIn_DisplayOverlayConfirmation.AddListenerSingle(DisplayOverlayConfirmation);
            this.btnRallyEnd.onClick.AddListener(OnBtnRallyEnd);
            this.btnRallyPause.onClick.AddListener(OnBtnRallyPause);
            this.btnRallyContinue.onClick.AddListener(OnBtnRallyContinue);
            this.btnClose.onClick.AddListener(OnBtnClose);
        }

        private void DisplayOverlayConfirmation(bool display)
        {
            this.gameObject.SetActive(display);
        }

        private void OnBtnRallyEnd()
        {
            PanelsHandler.EventIn_OnConfirmCloseRally.Invoke();
            DisplayOverlayConfirmation(false);
        }

        private void OnBtnRallyPause()
        {
            PanelsHandler.EventIn_OnConfirmCloseRally.Invoke();
            DisplayOverlayConfirmation(false);
        }

        private void OnBtnRallyContinue()
        {
            DisplayOverlayConfirmation(false);
        }

        private void OnBtnClose()
        {
            DisplayOverlayConfirmation(false);
        }
    }
}
