using GeoCoordinatePortable;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TownRally
{
    internal class MapDebugger : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmpDebug = null;
        [SerializeField] private Button btnDebug = null;
        [SerializeField] private MapsHandler mapsHandler = null;
        
        private void Awake()
        {
            this.btnDebug.onClick.AddListener(this.OnBtnDebug);
            this.mapsHandler.Init(false);
        }

        private void OnBtnDebug()
        {
            GeoCoordinate coordinates = new GeoCoordinate(47.075864f, 15.437085f);
            MapObjectsHandler.EventIn_AddMapObject.Invoke(coordinates, MapObject.Type.RallyMarker, "test1");
        }
    }
}
