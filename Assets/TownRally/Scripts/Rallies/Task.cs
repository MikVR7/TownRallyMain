using Newtonsoft.Json;
using static TownRally.Rally;

namespace TownRally
{
    public struct Task
    {
        public enum Type
        {
            None = 0,
            InfoScreen = 1,
            GotoDestination = 2,
            Game_Objectfind = 100,
        }

        [JsonIgnore] public string StationID { get; set; }
        public Type TType { get; set; }
        public Description[] Descr { get; set; }
    }
}
