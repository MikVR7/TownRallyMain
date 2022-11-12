using UnityEngine;

namespace TownRally
{
    internal class GlobalConfig : MonoBehaviour
    {
        internal static string SceneNameMain { get; } = "main";
        internal static string SceneNameARCam { get; } = "tr_ar_cam";
        internal static string SceneNameMap { get; } = "tr_map";

        internal static GeoPosition GeoPosHome { get; } = new GeoPosition() { Latitude = 47.06384f, Longitude = 15.44817f };
        internal static GeoPosition GeoPosKarmeliterplatz { get; } = new GeoPosition() { Latitude = 47.07397f, Longitude = 15.44034f };
    }
}
