using CodeEvents;
using System;
using System.Collections.Generic;
using static AndroidServices.AndroidBridge;

namespace AndroidServices
{
    public class Events
    {
        public class EventInOut_GetCurrentLocation : EventSystem<Action<Location>> { }
        public class EventInOut_GetBGLocationsList : EventSystem<Action<Locations>, Action<LocationListError>> { }
        public class EventIn_ClearBGLocationsList : EventSystem { }
        public class EventIn_SetMinTrackingDistance : EventSystem<float> { }
        public class EventIn_SetMinTrackingFetchTime : EventSystem<long> { }
        public class EventIn_StartForegroundService : EventSystem { }
        public class EventIn_StopForegroundService : EventSystem { }
    }
}
