using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TownRally
{
    public struct RallyStationTask
    {
        public enum Type
        {
            None = 0,
            InfoScreen = 1,
            GotoDestination = 2,
            Game_Objectfind = 100,
        }

        public Type TaskType { get; set; }
        public Vector2 DestinationPoint { get; set; }
        public string Description { get; set; }
    }
}
