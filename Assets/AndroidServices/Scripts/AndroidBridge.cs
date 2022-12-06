using Newtonsoft.Json;
using System;
using UnityEngine;
using static AndroidServices.Events;

namespace AndroidServices
{
    public class AndroidBridge : MonoBehaviour
    {
        public class Location
        {
            [JsonProperty("lat")] public double latitude { get; set; } = 0d;
            [JsonProperty("long")] public double longitude { get; set; } = 0d;
            [JsonProperty("time")] public string time { get; set; } = string.Empty;
        }

        public class Locations
        {
            [JsonProperty("location")] public Location[] locations { get; set; } = null;
        }

        public static EventInOut_GetCurrentLocation EventInOut_GetCurrentLocation = new EventInOut_GetCurrentLocation();
        public static EventInOut_GetBGLocationsList EventInOut_GetBGLocationsList = new EventInOut_GetBGLocationsList();
        public static EventIn_ClearBGLocationsList EventIn_ClearBGLocationsList = new EventIn_ClearBGLocationsList();
        public static EventIn_SetMinTrackingDistance EventIn_SetMinTrackingDistance = new EventIn_SetMinTrackingDistance();
        public static EventIn_SetMinTrackingFetchTime EventIn_SetMinTrackingFetchTime = new EventIn_SetMinTrackingFetchTime();
        public static EventIn_StartForegroundService EventIn_StartForegroundService = new EventIn_StartForegroundService();
        public static EventIn_StopForegroundService EventIn_StopForegroundService = new EventIn_StopForegroundService();

        public enum LocationListError {
            None = 0,
            ListEmpty = 1,
            ParsingError = 2,
            GPSDenied = 3,
            PermissionDenied = 4,
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        private readonly string JAVA_OBJECT_NAME = "com.abk.gps_forground.LocationPlugin";
        private AndroidJavaObject locationHelperClass = null;
        private AndroidJavaObject unityContext = null;
        private Action<Locations> onLocationDataSuccess;
        private Action<LocationListError> onLocationDataFail;

        private void Awake()
        {
            EventInOut_GetCurrentLocation.AddListener(GetCurrentLocation);
            EventInOut_GetBGLocationsList.AddListener(GetBGLocationsList);
            EventIn_ClearBGLocationsList.AddListener(ClearBGLocationsList);
            EventIn_SetMinTrackingDistance.AddListener(SetMinTrackingDistance);
            EventIn_SetMinTrackingFetchTime.AddListener(SetMinTrackingFetchTime);
            EventIn_StartForegroundService.AddListener(StartForegroundService);
            EventIn_StopForegroundService.AddListener(StopForegroundService);

            AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
            unityContext = ajo.Call<AndroidJavaObject>("getApplicationContext");
            locationHelperClass = new AndroidJavaObject(JAVA_OBJECT_NAME, ajo);
            locationHelperClass.Call("onCreate", unityContext);

            SetupAndroidTheme(ToARGB(Color.black), ToARGB(Color.black));
        }

        private void GetCurrentLocation(Action<Location> responseFunction)
        {
            string locationRaw = this.locationHelperClass.Call<string>("GetCurrentLocation");
            Location location = JsonConvert.DeserializeObject<Location>(locationRaw);
            location.time = DateTime.UtcNow.ToLongDateString();
            responseFunction.Invoke(location);
        }

        public void GetBGLocationsList(Action<Locations> onDataSuccess, Action<LocationListError> onFail)
        {
            this.onLocationDataSuccess = onDataSuccess;
            this.onLocationDataFail = onFail;
            locationHelperClass.Call("getCoordinatesList");
        }
        public void onLocationRetrieved(string data)
        {
            if(string.IsNullOrEmpty(data)) { onLocationDataFail?.Invoke(LocationListError.ListEmpty); return; }
            Locations locations = null;
            try { locations = JsonConvert.DeserializeObject<Locations>(data); }
            catch(Exception ex) { onLocationDataFail?.Invoke(LocationListError.ParsingError); return; }
            if((locations == null) || (locations.locations.Length == 0)) { onLocationDataFail?.Invoke(LocationListError.ListEmpty); }
            onLocationDataSuccess?.Invoke(locations);
        }
        public void onPermissionDenied(string data)
        {
            onLocationDataFail?.Invoke(LocationListError.PermissionDenied);
        }
        public void onGPSDenied(string data)
        {
            onLocationDataFail?.Invoke(LocationListError.GPSDenied);
        }

        private void SetMinTrackingDistance(float meters)
        {
            locationHelperClass.Call("setMinDistance", meters);
        }
        private void SetMinTrackingFetchTime(long milliseconds)
        {
            locationHelperClass.Call("setMinFetchTime", milliseconds);
        }
        private void StartForegroundService()
        {
            locationHelperClass.Call("startForegroundService");
        }
        private void StopForegroundService()
        {
            locationHelperClass.Call("stopForegroundService");
        }

        private void ClearBGLocationsList()
        {
            locationHelperClass.Call("clearCoordinatesList");
        }

        private void SetupAndroidTheme(int primaryARGB, int darkARGB, string label = null)
        {
            label = label ?? Application.productName;
            Screen.fullScreen = false;
            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaClass layoutParamsClass = new AndroidJavaClass("android.view.WindowManager$LayoutParams");
                int flagFullscreen = layoutParamsClass.GetStatic<int>("FLAG_FULLSCREEN");
                int flagNotFullscreen = layoutParamsClass.GetStatic<int>("FLAG_FORCE_NOT_FULLSCREEN");
                int flagDrawsSystemBarBackgrounds = layoutParamsClass.GetStatic<int>("FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS");
                AndroidJavaObject windowObject = activity.Call<AndroidJavaObject>("getWindow");
                windowObject.Call("clearFlags", flagFullscreen);
                windowObject.Call("addFlags", flagNotFullscreen);
                windowObject.Call("addFlags", flagDrawsSystemBarBackgrounds);
                int sdkInt = new AndroidJavaClass("android.os.Build$VERSION").GetStatic<int>("SDK_INT");
                int lollipop = 21;
                if (sdkInt > lollipop)
                {
                    windowObject.Call("setStatusBarColor", darkARGB);
                    string myName = activity.Call<string>("getPackageName");
                    AndroidJavaObject packageManager = activity.Call<AndroidJavaObject>("getPackageManager");
                    AndroidJavaObject drawable = packageManager.Call<AndroidJavaObject>("getApplicationIcon", myName);
                    AndroidJavaObject taskDescription = new AndroidJavaObject("android.app.ActivityManager$TaskDescription", label, drawable.Call<AndroidJavaObject>("getBitmap"), primaryARGB);
                    activity.Call("setTaskDescription", taskDescription);
                }
            }));
        }

        private int ToARGB(Color color)
        {
            Color32 c = (Color32)color;
            byte[] b = new byte[] { c.b, c.g, c.r, c.a };
            return BitConverter.ToInt32(b, 0);
        }
#endif
    }
}
