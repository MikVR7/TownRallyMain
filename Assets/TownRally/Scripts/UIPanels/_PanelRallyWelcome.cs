//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//namespace TownRally
//{
//    internal class PanelRallyWelcome : APanel
//    {
//        [SerializeField] private Button btnContinue = null;

//        internal override void Init(PanelsHandler.PanelType panelType)
//        {
//            base.Init(panelType);
//            this.btnContinue.onClick.AddListener(OnBtnContinue);
//        }

//        private void OnBtnContinue()
//        {
//            PanelsHandler.EventIn_SetPanel.Invoke(PanelsHandler.PanelType.TaskGotoDestination);
//        }
//    }
//}
