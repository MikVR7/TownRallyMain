
using System.Collections.Generic;

namespace TownRally
{
    public struct Rally
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public List<string> Description { get; set; }
        public List<RallyStation> Stations { get; set; }
    }
}
