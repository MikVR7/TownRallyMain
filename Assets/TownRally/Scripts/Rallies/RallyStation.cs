using System.Collections.Generic;

namespace TownRally
{
    public class RallyStation
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int[] AssignmentIDs { get; set; }
    }
}
