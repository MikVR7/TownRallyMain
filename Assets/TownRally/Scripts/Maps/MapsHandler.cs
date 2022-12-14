using AndroidServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static AndroidServices.AndroidBridge;

namespace TownRally
{
    internal class MapsHandler : MonoBehaviour
    {
        internal static EventIn_SetMapPosition EventIn_SetMapPosition = new EventIn_SetMapPosition();
        internal static EventIn_SetMapCurrentGPSPosition EventIn_SetMapCurrentGPSPosition = new EventIn_SetMapCurrentGPSPosition();
        internal static EventIn_DisplayMap EventIn_DisplayMap = new EventIn_DisplayMap();

        [SerializeField] private OnlineMaps onlineMaps = null;
        [SerializeField] private OnlineMapsTileSetControl onlineMapsTileset = null;
        [SerializeField] private OnlineMapsControlBase onlineMapsControlBase = null;
        [SerializeField] private OnlineMapsMarker3DManager onlineMapsMarker3DManager = null;
        [SerializeField] private Camera cameraMap = null;
        [SerializeField] private GameObject goEventSystem = null;
        [SerializeField] private UniversalAdditionalCameraData camData = null;
        [SerializeField] private MapObjectsHandler mapObjectsHandler = null;
        [SerializeField] private Light mapLight = null;

        internal void Init(bool isSceneMain)
        {
            EventIn_SetMapPosition.AddListenerSingle(SetMapPosition);
            EventIn_SetMapCurrentGPSPosition.AddListenerSingle(SetMapCurrentGPSPosition);
            EventIn_DisplayMap.AddListenerSingle(DisplayMap);

            this.mapObjectsHandler.Init(this.onlineMapsMarker3DManager);
            this.camData.renderType = isSceneMain ? CameraRenderType.Overlay : CameraRenderType.Base;
            this.goEventSystem.SetActive(!isSceneMain);
            this.DisplayMap(!isSceneMain);
            this.mapLight.enabled = isSceneMain;

            onlineMapsTileset.OnMapClick += OnMapClick;
        }

        private void OnMapClick()
        {
            Vector2 location = onlineMapsControlBase.GetCoords();
            Debug.Log("ON MAP CLICK! " + location.x + " " + location.y);
        }

        private void DisplayMap(bool display)
        {
            this.gameObject.SetActive(display);
        }

        private void SetMapPosition(Location location)
        {
            this.onlineMaps.SetPosition(location.longitude, location.latitude);
        }

        private void SetMapCurrentGPSPosition()
        {
            AndroidBridge.EventInOut_GetCurrentLocation.Invoke(CurrentPosReceived);
        }
        private void CurrentPosReceived(Location location)
        {
            SetMapPosition(location);
        }
    }
}
