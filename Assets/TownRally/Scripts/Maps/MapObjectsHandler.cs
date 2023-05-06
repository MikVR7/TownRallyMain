using GeoCoordinatePortable;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TownRally
{
    internal class MapObjectsHandler : MonoBehaviour
    {
        private static MapObjectsHandler Instance = null;
        internal static EventIn_AddMapObject EventIn_AddMapObject = new EventIn_AddMapObject();
        internal static EventIn_SetMapObjectPosition EventIn_SetMapObjectPosition = new EventIn_SetMapObjectPosition();
        internal static List<MapObject> VarOut_GetStations()
        {
            List<MapObject> stations = new List<MapObject>();
            foreach(string id in Instance.mapObjects.Keys)
            {
                if(Instance.mapObjects[id].VarOut_ObjectType.Equals(MapObject.Type.GotoDestination)) {
                    stations.Add(Instance.mapObjects[id]);
                }
            }
            return stations;
        }


        [SerializeField] private GameObject prefabMapObject = null;
        private Dictionary<string, MapObject> mapObjects = new Dictionary<string, MapObject>();
        private OnlineMapsMarker3DManager onlineMapsMarker3DManager = null;
        private Vector2 geoCoords = Vector2.zero;

        internal void Init(OnlineMapsMarker3DManager onlineMapsMarker3DManager)
        {
            Instance = this;
            EventIn_AddMapObject.AddListenerSingle(AddMapObject);
            EventIn_SetMapObjectPosition.AddListenerSingle(SetMapObjectPosition);
            this.onlineMapsMarker3DManager = onlineMapsMarker3DManager;

            
            this.AddMapObject(Settings.GeoPosHome, MapObject.Type.Character, Settings.MapObjectNameCharMain);
            /*this.AddMapObject(GlobalConfig.GeoPosKarmeliterplatz, MapObject.Type.GotoDestination, GlobalConfig.MapObjectNameStationSuffix + "1");
            this.AddMapObject(GlobalConfig.GeoPosUhrturm, MapObject.Type.GotoDestination, GlobalConfig.MapObjectNameStationSuffix + "2");
            */
        }

        private void AddMapObject(GeoCoordinate geoCoord, MapObject.Type objectType, string id)
        {
            OnlineMapsMarker3D marker = this.onlineMapsMarker3DManager.Create(geoCoord.Longitude, geoCoord.Latitude, prefabMapObject);
            MapObject mapObject = marker.transform.GetComponent<MapObject>();
            mapObject.Init(id, marker, objectType, geoCoord);
            this.mapObjects.Add(id, mapObject);
        }

        private void SetMapObjectPosition(GeoCoordinate geoCoord, string id)
        {
            if (this.mapObjects.ContainsKey(id))
            {
                this.mapObjects[id].SetPosition(geoCoord);
            }
            else
            {
                Debug.LogWarning("Object not found: " + id);
            }
        }
    }
}
