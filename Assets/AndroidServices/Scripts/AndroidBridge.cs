using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TownRally
{
    internal class AndroidBridge : MonoBehaviour
    {
        private static readonly string JAVA_OBJECT_NAME = "com.abk.gps_forground.LocationPlugin";
        private AndroidJavaObject locationHelperClass = null;

        private void Awake()
        {
            //DontDestroyOnLoad(gameObject);
            AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
            locationHelperClass = new AndroidJavaObject(JAVA_OBJECT_NAME, ajo);
        }

        public string GetLocation()
        {
            return locationHelperClass.Call<string>("getLocation");
        }

        public void GetLocationList(Action<string> onDataSuccess, Action<string> onFail)
        {
            //LocationPluginHelper.onDataSuccess = onDataSuccess;
            //LocationPluginHelper.onFail = onFail;
            //locationHelperClass.Call("getCoordinatesList");
        }

        public void setMinDistance(float distance)
        {
            locationHelperClass.Call("setMinDistance", distance);
        }
        public void setMinFetchTime(long time)
        {
            locationHelperClass.Call("setMinFetchTime", time);
        }
        public void startForegroundService()
        {
            locationHelperClass.Call("startForegroundService");
        }
        public void stopForegroundService()
        {
            locationHelperClass.Call("stopForegroundService");
        }

        public void clearCoordinatesList()
        {
            locationHelperClass.Call("clearCoordinatesList");
        }
    }
}
