using CodeEvents;
using Firebase.Database;
using GeoCoordinatePortable;
using System;
using System.Collections.Generic;
using UnityEngine;
using static AndroidServices.AndroidBridge;
using static TownRally.RallyCreator;

namespace TownRally
{
    // TownRallyHandler.cs
    internal class EventOut_OnUpdate : EventSystem { }

    // TaskBarHandler.cs
    internal class EventIn_SetActiveBtnBack : EventSystem <bool> { }
    internal class EventIn_SetTaskBarUsername : EventSystem<string> { }
    internal class EventIn_SetTaskBarRallyname : EventSystem<string> { }
    internal class EventOut_OnBtnDebug : EventSystem { }

    // PanelsHandlerBody.cs
    internal class EventIn_OnBtnPanelBack : EventSystem { }
    internal class EventIn_SetPanel : EventSystem<PanelsHandler.PanelType> { }

    // RalliesHandler.cs
    //internal class EventIn_SetCurrentRally : EventSystem<int> { }
    //internal class EventOut_RallyChanged : EventSystem<Rally> { }
    internal class EventIn_LoadRalliesList : EventSystem { }
    internal class EventIn_SetCurrentRally : EventSystem<string> { }
    internal class EventOut_RalliesLoadingDone : EventSystem { }
    internal class EventOut_StationsLoadingDone : EventSystem { }
    internal class EventOut_TasksLoadingDone : EventSystem { }
    //internal class EventIn_CurrentTaskFinished : EventSystem { }
    //internal class EventOut_RallyStationTaskChanged : EventSystem { }

    // RallyCreator.cs
    internal class EventIn_AddNewRallyToServer : EventSystem<NewRallyType> { }

    // OverlayHandler.cs
    internal class EventIn_DisplayOverlay : EventSystem<OverlaysHandler.OverlayType, string, Action, Action> { }

    // OverlayConfirmation.cs
    internal class EventIn_DisplayOverlaySpecific : EventSystem<string, Action, Action> { }

    // MapsHandler.cs
    internal class EventIn_SetMapPosition : EventSystem<Location> { }
    internal class EventIn_SetMapCurrentGPSPosition : EventSystem { }
    internal class EventIn_DisplayMap : EventSystem<bool> { }
    internal class EventIn_SetMapZoom : EventSystem<float> { }
    internal class EventOut_MapOnChangeZoom : EventSystem<float> { }
    internal class EventOut_OnMapClick : EventSystem<Vector2> { }

    // MapObjectsHandler.cs
    internal class EventIn_AddMapObject : EventSystem<GeoCoordinate, MapObject.Type, string> { }
    internal class EventIn_SetMapObjectPosition : EventSystem<GeoCoordinate, string> { }

    // DatabaseHandler.cs
    internal class EventIn_SaveRallyWhole : EventSystem<string, Rally, Dictionary<string, Station>, Dictionary<string, Task>> { }
    internal class EventInOut_LoadDBRalliesAll : EventSystem<Action<Dictionary<string, Rally>>> { }
    internal class EventInOut_LoadDBRallyStations : EventSystem<string, Action<Dictionary<string, Station>>> { }
    internal class EventInOut_LoadDBRallyStationTasks : EventSystem<string, int, Action<Dictionary<string, Task>>> { }
    internal class EventIn_SaveImage : EventSystem<string, Texture2D> { }
    internal class EventInOut_LoadImage : EventSystem<string, Action<Texture2D>> { }

    // Settings.cs
    //internal class EventOut_SettingsChangedStr :EventSystem<string> { }
    //internal class EventOut_SettingsChangedInt : EventSystem<int> { }
    //internal class EventOut_SettingsChangedBool : EventSystem<bool> { }
    internal class EventOut_ValueChangedEvent : EventSystem { }
}
