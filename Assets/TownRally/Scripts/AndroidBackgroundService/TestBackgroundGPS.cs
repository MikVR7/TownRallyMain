using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

namespace TownRally
{
    public class TestBackgroundGPS : MonoBehaviour
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
        private const string PlayerPrefsTotalSteps = "totalSteps";
        private const string PackageName = "com.tokele.townrallyandroidlib.UnityAndroidBridge";
        //private const string PackageName = "com.kdg.toast.plugin.Bridge";
        private const string UnityDefaultJavaClassName = "com.unity3d.player.UnityPlayer";
        private const string CustomClassReceiveActivityInstanceMethod = "ReceiveActivityInstance";
        private const string CustomClassStartServiceMethod = "StartService";
        private const string CustomClassStopServiceMethod = "StopService";
        private const string CustomClassGetCurrentStepsMethod = "GetCurrentSteps";
        //private const string CustomClassSyncDataMethod = "SyncData";


        private void Awake()
        {
            SendActivityReference(PackageName);
            GetCurrentSteps();

            this.btnTest1.onClick.AddListener(StartService);
            this.btnTest1.onClick.AddListener(StopService);
            this.btnTest1.onClick.AddListener(SyncData);
        }


        private void SendActivityReference(string packageName)
        {
            unityClass = new AndroidJavaClass(UnityDefaultJavaClassName);
            unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
            customClass = new AndroidJavaClass(packageName);
            customClass.CallStatic(CustomClassReceiveActivityInstanceMethod, unityActivity);
        }

        public void StartService()
        {
            customClass.CallStatic(CustomClassStartServiceMethod);
            GetCurrentSteps();
        }

        public void StopService()
        {
            customClass.CallStatic(CustomClassStopServiceMethod);
        }

        public void GetCurrentSteps()
        {
            int? stepsCount = customClass.CallStatic<int>(CustomClassGetCurrentStepsMethod);
            tmpText.text = stepsCount.ToString();
        }

        public void SyncData()
        {
            //var data = customClass.CallStatic<string>(CustomClassSyncDataMethod);

            //var parsedData = data.Split('#');
            //var dateOfSync = parsedData[0] + " - " + parsedData[1];
            //syncedDateText.text = dateOfSync;
            //var receivedSteps = int.Parse(parsedData[2]);
            //var prefsSteps = PlayerPrefs.GetInt(PlayerPrefsTotalSteps, 0);
            //var prefsStepsToSave = prefsSteps + receivedSteps;
            //PlayerPrefs.SetInt(PlayerPrefsTotalSteps, prefsStepsToSave);
            //totalStepsText.text = prefsStepsToSave.ToString();

            //GetCurrentSteps();
        }


        ////////////private AndroidJavaClass javaClass = null;

        ////////////// Start is called before the first frame update
        ////////////void Start()
        ////////////{
        ////////////    Debug.Log("PROGRAM STARTED!");
        ////////////    this.btnTest1.onClick.AddListener(OnBtnTest1);
        ////////////    this.btnTest2.onClick.AddListener(OnBtnTest2);

        ////////////    // Get a reference to our Java class
        ////////////    javaClass = new AndroidJavaClass("com.tokele.townrallyandroidlib.UnityAndroidBridge");

        ////////////    // We setup a simple UI in Unity Editor to display the result on button click.
        ////////////    // Here we just get references to UI objects and set up a listener.
        ////////////    //text = GetComponentInChildren();
        ////////////    //var button = GetComponentInChildren();
        ////////////    //button.onClick.AddListener(onUiButtonClicked);
        ////////////}

        //////////////private AndroidJavaObject androidPlugin = null;
        ////////////private AndroidJavaObject unityContext = null;
        ////////////private AndroidJavaObject unityActivity = null;
        ////////////private AndroidJavaClass unityClass = null;

        ////////////private void OnBtnTest1()
        ////////////{
        ////////////    // Call the static method using the java class reference.
        ////////////    // We call the Generate method that returns an integer and give the maximum range as an argument
        ////////////    int randomNmb = this.javaClass.CallStatic<int>("GenerateRandomNr", 100);
        ////////////    this.tmpText.text = randomNmb.ToString();
        ////////////}

        ////////////private void OnBtnTest2()
        ////////////{
        ////////////    //Get unity activity and context
        ////////////    unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        ////////////    unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        ////////////    unityContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");

        ////////////    //this.javaClass.CallStatic("
        
        
        
        ////////////", unityContext);
        ////////////    this.javaClass.CallStatic("getLocation", unityContext);

        ////////////}

        ////////////////////private void OnBtnTest1()
        ////////////////////{
        ////////////////////    //Get unity activity and context
        ////////////////////    unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        ////////////////////    unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        ////////////////////    unityContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");

        ////////////////////    //Create android plugin object
        ////////////////////    //androidPlugin = new AndroidJavaObject("com.androidplugin.stepcounterlibrary.ApiStepCounter");
        ////////////////////    androidPlugin = new AndroidJavaObject("com.example.gpsupdater.LocationHelper");

        ////////////////////    //Set activity and context to the module 
        ////////////////////    androidPlugin.Call("setActivity", unityActivity);
        ////////////////////    androidPlugin.Call("setContext", unityContext);

        ////////////////////    //////////Debug.Log("UIIIIIIIII!");
        ////////////////////    //////////ajc = new AndroidJavaClass("com.example.gpsupdater.LocationHelper");
        ////////////////////    //////////AndroidJavaObject jo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
        ////////////////////    //////////NativePlugin = pluginClass.CallStatic<AndroidJavaObject>("instance");
        ////////////////////    //////////NativePlugin.Call("setContext", activityContext);
        ////////////////////    //////////return;
        ////////////////////    androidPlugin.Call("StartUpdates");

        ////////////////////    ////////////string value = ajc.CallStatic<string>("GetGPSData");
        ////////////////////    //////////Debug.Log("OK DONE1: "/* + value*/);
        ////////////////////}
        //////////////////////private AndroidJavaClass ajc;
        ////////////////////private void OnBtnTest2()
        ////////////////////{

        ////////////////////    string value = androidPlugin.Call<string>("GetGPSData");
        ////////////////////    Debug.Log("OK DONE2: " + value);
        ////////////////////}

        ////////////// Update is called once per frame
        ////////////void Update()
        ////////////{

        ////////////}
    }
}
