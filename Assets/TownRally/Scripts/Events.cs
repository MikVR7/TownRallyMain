using CodeEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TownRally
{
    // TownRallyHandler.cs
    internal class EventOut_OnUpdate : EventSystem { }

    // TaskBarHandler.cs
    internal class EventIn_SetActiveBtnBack : EventSystem <bool> { }
    internal class EventIn_SetTaskBarUsername : EventSystem<string> { }
    internal class EventIn_SetTaskBarRallyname : EventSystem<string> { }

    // PanelsHandlerBody.cs
    internal class EventIn_OnBtnPanelBack : EventSystem { }
    internal class EventIn_SetPanel : EventSystem<PanelsHandler.PanelType> { }
    internal class EventIn_OnConfirmCloseRally : EventSystem { }

    // RalliesHandler.cs
    internal class EventIn_SetCurrentRally : EventSystem<int> { }
    internal class EventOut_RallyChanged : EventSystem<Rally> { }
    internal class EventIn_CurrentTaskFinished : EventSystem { }
    internal class EventOut_RallyStationTaskChanged : EventSystem { }
    //internal class EventOut_RallyStationChanged : EventSystem { }

    // OverlayConfirmation.cs
    internal class EventIn_DisplayOverlayConfirmation : EventSystem<bool> { }
}
