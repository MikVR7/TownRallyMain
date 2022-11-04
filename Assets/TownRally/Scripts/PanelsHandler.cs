using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace TownRally
{
    internal class PanelsHandler : SerializedMonoBehaviour
    {
        internal enum PanelType
        {
            None = 0,
            Login = 1,
            RallySelection = 2,
            StartScreen = 3,
            RallyInfo = 4,
            RallyMap = 5,
            Rally = 6,
        }

        internal static EventIn_OnBtnPanelBack EventIn_OnBtnPanelBack = new EventIn_OnBtnPanelBack();
        internal static EventIn_OnConfirmCloseRally EventIn_OnConfirmCloseRally = new EventIn_OnConfirmCloseRally();
        internal static EventIn_SetPanel EventIn_SetPanel = new EventIn_SetPanel();

        [SerializeField] private Dictionary<PanelType, APanel> panels = new Dictionary<PanelType, APanel>();

        private PanelType currentPanel = PanelType.Login;
        private List<PanelType> panelQueue = new List<PanelType>();

        internal void Init()
        {
            this.panels.Keys.ForEach(i => this.panels[i].Init(i));
            EventIn_OnBtnPanelBack.AddListenerSingle(OnBtnPanelBack);
            EventIn_SetPanel.AddListenerSingle(SetPanel);
            SetPanel(PanelType.Login);
        }

        private void OnBtnPanelBack()
        {
            if (this.panelQueue.Count >= 2)
            {
                if (this.panelQueue[this.panelQueue.Count - 1] == PanelType.Rally)
                {
                    EventIn_OnConfirmCloseRally.AddListenerSingle(OnConfirmCloseRally);
                    OverlayConfirmation.EventIn_DisplayOverlayConfirmation.Invoke(true);
                }
                else
                {
                    PanelType previousPanelType = this.panelQueue[this.panelQueue.Count - 2];
                    this.panelQueue.RemoveAt(this.panelQueue.Count - 1);
                    this.panelQueue.RemoveAt(this.panelQueue.Count - 1);
                    SetPanel(previousPanelType);
                }
            }
        }

        private void OnConfirmCloseRally()
        {
            PanelType previousPanelType = this.panelQueue[this.panelQueue.Count - 2];
            this.panelQueue.RemoveAt(this.panelQueue.Count - 1);
            this.panelQueue.RemoveAt(this.panelQueue.Count - 1);
            SetPanel(previousPanelType);
        }

        private void SetPanel(PanelType nextPanel)
        {
            this.currentPanel = nextPanel;
            AddPanelToPanelQueue();
            this.panels.Keys.ForEach(i => this.panels[i].SetActive(nextPanel.Equals(i)));
            TaskBarHandler.EventIn_SetActiveBtnBack.Invoke(this.panelQueue.Count > 1);
            Debug.Log("PANELS IN QUEUE: " + this.panelQueue.Count);
        }

        private void AddPanelToPanelQueue()
        {
            this.panelQueue.Add(this.currentPanel);
        }
    }
}
