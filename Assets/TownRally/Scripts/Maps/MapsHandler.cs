using CodeEvents;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

namespace TownRally
{
    //public struct GeoPosition
    //{
    //    public float Latitude;
    //    public float Longitude;
    //    public float Altitude;
    //}
    internal class MapsHandler : MonoBehaviour
    {
        internal static MapsHandler Instance = null;

        [SerializeField] private OnlineMaps onlineMaps = null;
        [SerializeField] private OnlineMapsTileSetControl onlineMapsTileset = null;
        [SerializeField] private OnlineMapsMarker3DManager onlineMapsMarker3DManager = null;
        [SerializeField] private Camera cameraMap = null;
        [SerializeField] private EventSystem eventSystem = null;
        [SerializeField] private UniversalAdditionalCameraData camData = null;
        [SerializeField] private MapObjectsHandler mapObjectsHandler = null;

        private void Awake()
        {
            Instance = this;
            Scene activeScene = SceneManager.GetActiveScene();
            if (activeScene.name.Equals(GlobalConfig.SceneNameMap))
            {
                camData.renderType = CameraRenderType.Base;
                Init();
            }
            else if (activeScene.name.Equals(GlobalConfig.SceneNameMain))
            {
                camData.renderType = CameraRenderType.Overlay;
            }
        }

        internal void Init()
        {
            this.onlineMapsTileset.activeCamera = this.cameraMap;
            this.mapObjectsHandler.Init(this.onlineMapsMarker3DManager);
        }

        internal Camera VarOut_Camera() { return this.cameraMap; }
    }
}
