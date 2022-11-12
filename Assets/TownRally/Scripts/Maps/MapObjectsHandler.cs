using System.Collections.Generic;
using UnityEngine;

namespace TownRally
{
    internal class MapObjectsHandler : MonoBehaviour
    {
        [SerializeField] private GameObject prefabMapObject = null;
        private List<MapObject> mapObjects = new List<MapObject>();
        private OnlineMapsMarker3DManager onlineMapsMarker3DManager = null;
        private Vector2 geoCoords = Vector2.zero;

        internal void Init(OnlineMapsMarker3DManager onlineMapsMarker3DManager)
        {
            this.onlineMapsMarker3DManager = onlineMapsMarker3DManager;
            this.AddMapObject(GlobalConfig.GeoPosHome, MapObject.Type.Character);
            this.AddMapObject(GlobalConfig.GeoPosKarmeliterplatz, MapObject.Type.GotoDestination);
        }

        private void AddMapObject(GeoPosition geoPos, MapObject.Type objectType)
        {
            OnlineMapsMarker3D marker = this.onlineMapsMarker3DManager.Create(geoPos.Longitude, geoPos.Latitude, prefabMapObject);
            MapObject mapObject = marker.transform.GetComponent<MapObject>();
            mapObject.Init(marker, objectType);
            this.mapObjects.Add(mapObject);
        }
    }
}
