using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TownRally
{
    public struct RallyStationTask
    {
        public enum RallyTask
        {
            None = 0,
            Welcome = 1,
            GotoDestination = 2,
        }

        public RallyTask TaskType { get; set; }
        public List<Vector2> DestinationPoints { get; set; }
        public string Description { get; set; }
    }
}
