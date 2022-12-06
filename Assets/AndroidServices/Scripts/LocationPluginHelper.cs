using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndroidServices
{
    public static class LocationPluginHelper
    {
        // Game object is created to receive async messages
        private const string GAME_OBJECT_NAME = "LocationPluginListener";
        private static GameObject gameObject;
        private static AndroidJavaObject locationHelperClass;

        // Save a reference of the callback to pass async messages
        private static Action<string> onDataSuccess;
        private static Action<string> onFail;

        // Android only variables
        private const string JAVA_OBJECT_NAME = "com.abk.gps_forground.LocationPlugin";
        //private const string JAVA_OBJECT_NAME = "com.abk.gps_forground.LocationPluginUnityActivity";

        static LocationPluginHelper()
        {
            Debug.Log("Location Plugin Helper Constructor");
            // Create Game Object to allow sending messages from Java to Unity
            gameObject = new GameObject();

            // Object name must match UnitySendMessage call in Java
            gameObject.name = GAME_OBJECT_NAME;

            // Attach this class to allow for handling of callbacks from Java
            gameObject.AddComponent<CallbackHandler>();

            // Do not destroy when loading a new scene
            UnityEngine.Object.DontDestroyOnLoad(gameObject);

            AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");

            locationHelperClass = new AndroidJavaObject(JAVA_OBJECT_NAME, ajo);
        }

        public static string GetLocation()
        {
#if UNITY_ANDROID
            return locationHelperClass.Call<string>("getLocation");
#endif
        }

        public static void GetLocationList(Action<string> onDataSuccess, Action<string> onFail)
        {
            LocationPluginHelper.onDataSuccess = onDataSuccess;
            LocationPluginHelper.onFail = onFail;

#if UNITY_ANDROID
            locationHelperClass.Call("getCoordinatesList");
#endif
        }

        public static void setMinDistance(float distance)
        {
#if UNITY_ANDROID
            locationHelperClass.Call("setMinDistance", distance);
#endif
        }
        public static void setMinFetchTime(long time)
        {
#if UNITY_ANDROID
            locationHelperClass.Call("setMinFetchTime", time);
#endif
        }
        public static void startForegroundService()
        {
#if UNITY_ANDROID
            locationHelperClass.Call("startForegroundService");
#endif
        }
        public static void stopForegroundService()
        {
#if UNITY_ANDROID
            locationHelperClass.Call("stopForegroundService");
#endif
        }

        public static void clearCoordinatesList()
        {
#if UNITY_ANDROID
            locationHelperClass.Call("clearCoordinatesList");
#endif
        }



        public class CallbackHandler : MonoBehaviour
        {
            private void HandleException(string exception)
            {
                throw new Exception(exception);
            }

            public void onLocationRetrieved(string data)
            {
                Debug.Log("onLocationRetrieved UNITY >>>> " + data);
                onDataSuccess?.Invoke(data);
            }

            public void onPermissionDenied(string data)
            {
                Debug.Log("onPermissionDenied UNITY >>>> " + data);
                onFail?.Invoke(data);
            }

            public void onGPSDenied(string data)
            {
                Debug.Log("onGPSDenied UNITY >>>> " + data);
                onFail?.Invoke(data);
            }
        }
    }
}