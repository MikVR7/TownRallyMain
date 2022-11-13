using GeoCoordinatePortable;
using UnityEngine;

namespace TownRally
{
    internal class GlobalConfig : MonoBehaviour
    {
        internal static string SceneNameMain { get; } = "main";
        internal static string SceneNameARCam { get; } = "tr_ar_cam";
        internal static string SceneNameMap { get; } = "tr_map";

        internal static GeoCoordinate GeoPosHome { get; } = new GeoCoordinate() { Latitude = 47.06384f, Longitude = 15.44817f };
        internal static GeoCoordinate GeoPosChanni { get; } = new GeoCoordinate() { Latitude = 47.06396f, Longitude = 15.44811f };
        internal static GeoCoordinate GeoPosKarmeliterplatz { get; } = new GeoCoordinate() { Latitude = 47.07397f, Longitude = 15.44034f };
        internal static GeoCoordinate GeoPosUhrturm { get; } = new GeoCoordinate() { Latitude = 47.07369f, Longitude = 15.43770f };


        internal static string MapObjectNameCharMain { get; } = "char_main";
        internal static string MapObjectNameStationSuffix { get; } = "station_";
        internal static string MapObjectNameWaypointSuffix { get; } = "wp_";

    }
}
