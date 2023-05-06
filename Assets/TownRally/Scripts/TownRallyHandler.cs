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
        [SerializeField] private MapsHandler mapsHandler = null;
        [SerializeField] private CamerasHandler camerasHandler = null;
        [SerializeField] private OverlaysHandler overlaysHandler = null;
        private DatabaseHandler databaseHandler = new DatabaseHandler();
        private RalliesHandler ralliesHandler = new RalliesHandler();
        private PlayerPrefsHandler playerPrefsHandler = new PlayerPrefsHandler();

        private void Awake()
        {
            //#if UNITY_ANDROID
            //            Screen.fullScreen = false;
            //#endif
            Settings.Init();
            this.overlaysHandler.Init();
            this.databaseHandler.Init();
            this.playerPrefsHandler.Init();
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
