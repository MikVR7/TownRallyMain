using System;
using UnityEngine;

namespace TownRally
{
    internal class TownRallyHandler : MonoBehaviour
    {
        internal static EventOut_OnUpdate EventOut_OnUpdate = new EventOut_OnUpdate();
        //internal static event Action EventOut_OnUpdate;

        [SerializeField] private PanelsHandler panelsHandlerBody = null;
        [SerializeField] private TaskBarHandler taskBarHandler = null;
        [SerializeField] private OverlayConfirmation overlayConfirmation = null;
        [SerializeField] private MapsHandler mapsHandler = null;
        [SerializeField] private CamerasHandler camerasHandler = null;
        private RalliesHandler ralliesHandler = new RalliesHandler();

        private void Awake()
        {
//#if UNITY_ANDROID
//            Screen.fullScreen = false;
//#endif
            this.camerasHandler.Init();
            this.ralliesHandler.Init();
            this.taskBarHandler.Init();
            this.panelsHandlerBody.Init();
            this.mapsHandler.Init(true);
        }

        private void Update()
        {
            EventOut_OnUpdate.Invoke();
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                PanelsHandler.EventIn_OnBtnPanelBack.Invoke();
            }
        }
    }
}
