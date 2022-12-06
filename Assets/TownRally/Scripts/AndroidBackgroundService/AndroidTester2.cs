using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TownRally
{
    public class AndroidTester2 : MonoBehaviour
    {
        [SerializeField] private Button btnTest1 = null;
        [SerializeField] private Button btnTest2 = null;
        [SerializeField] private Button btnTest3 = null;
        [SerializeField] private TextMeshProUGUI tmpText = null;
        [SerializeField] private TextMeshProUGUI syncedDateText = null;
        [SerializeField] private TextMeshProUGUI totalStepsText = null;

        private AndroidJavaClass unityClass;
        private AndroidJavaObject unityActivity;
        private AndroidJavaClass customClass;

        private const string PackageName = "com.tokele.townrallyandroidlib.MainActivity";
        private const string UnityDefaultJavaClassName = "com.unity3d.player.UnityPlayer";
        private const string CustomClassReceiveActivityInstanceMethod = "ReceiveActivityInstance";

        // Start is called before the first frame update
        void Start()
        {
            this.btnTest1.onClick.AddListener(StartService);
            this.btnTest2.onClick.AddListener(StopService);
            this.btnTest3.onClick.AddListener(SyncData);



            // LocationService : Service()
            //  onCreate()
            //  createNotificationChanel
            //  onStartCommand(intent: Intent?, flags: Int, startId: Int): Int
            //  onDestroy()
            //  OnBind(intent: Intent?): IBinder?
            //  requestLocationUpdates()
            //  locationCallback = object:LocationCallback()
            //  removeLocationUpdates()

            // Manifest:
            //  android:name=".services.LocationService"

            // LocationPluginUnityActivity extends UnityPlayerActivity
            //  onActivityResult(int requestCode, int resultCode, Intent data)


            //package com.abk.gps_forground
            // LocationFragment : Fragment()
            //  var mLocationService: LocationService = LocationService()
            //  lateinit var mServiceIntent: Intent
            //  private var mSettingsClient: SettingsClient? = null
            //  onCreate(savedInstanceState: Bundle?)
            //  private getLocation()
            //  onActivityResult(requestCode: Int, resultCode: Int, data: Intent?)
            //  private fun takePermission()
            //  fun stopLocationUpdates()
            //  private fun checkPermissions(): Boolean
            //  fun removeLocationUpdates()
            //  override fun onStop()
            //  override fun onDestroy()
            //  


            //import static com.abk.gps_forground.utils.FileHelperKt.getAllLocationData;
            //LocationPlugin
            // public LocationPlugin(Activity activity)
            // public void setMinDistance(float minDistance)
            // public void setMinFetchTime(long minTime)
            // public void startForegroundService()
            // public void stopForegroundService()
            // public String getLocation()
            // public void getCoordinatesList()
            // public void clearCoordinatesList()
            // 


            // fun getAllLocationData(context: Context): String { (2901)
            // fun writeDataToFile(
            // fun clearAllData(context: Context)

            //UnityCallbacks
            // companion object {
            // const val CALLBACK_OBJ: String = "LocationPluginListener"
            // const val onLocationRetrieved: String = "onLocationRetrieved"
            // const val onPermissionDenied: String = "onPermissionDenied"
            // const val onGPSDenied: String = "onGPSDenied"
            //
            // fun locationDataRetrieved(response: String)
            // fun permissionDenied(permission: String)
            // fun gpsDenied()


            //Utils (3085)

            // UNITY: (3566)


        }

        private AndroidJavaObject _object;
        private AndroidJavaClass _staticClass;
        private void SendActivityReference(string packageName)
        {
            //unityClass = new AndroidJavaClass(UnityDefaultJavaClassName);
            //unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
            //customClass = new AndroidJavaClass(packageName);
            //customClass.Call(CustomClassReceiveActivityInstanceMethod, unityActivity);

            //_object = new AndroidJavaObject("com.tokele.townrallyandroidlib.MainActivity");
            _staticClass = new AndroidJavaClass(packageName);
            //var defaultName = _object.Call<string>("getName");
            //Debug.Log("START GET DEFAUL NAME: " + defaultName);
        }

        private void StartService()
        {
            SendActivityReference(PackageName);
            Debug.Log("START SERVICE!");
        }

        private void StopService()
        {
            _object.Call("callNormalFunc");
            Debug.Log("STOP SERVICE!");
        }

        private void SyncData()
        {
            var companionObject = _staticClass.GetStatic<AndroidJavaObject>("Companion");
            var newName = companionObject.Call<AndroidJavaObject>("callStaticCompanionFunc").Call<string>("getName");
            Debug.Log("SYNC DATA " + newName);
        }
    }
}
