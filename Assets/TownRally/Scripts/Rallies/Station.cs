using Newtonsoft.Json;
using System.Collections.Generic;

namespace TownRally
{
    public struct Station
    {
        [JsonIgnore] public string RallyID { get; set; }
        public int[] NextStations { get; set; }
        public int FinalStation { get; set; }
        public KeyValuePair<float, float> Pos { get; set; }
    }
}
