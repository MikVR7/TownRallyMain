using System.Collections.Generic;

namespace TownRally
{
    public class RallyStation
    {
        //public int ID { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        public List<RallyStationTask> Tasks { get; set; }
        public List<int> NextPossibleStationIDs { get; set; }
        internal int currentTask;
        internal bool isFinished;
    }
}
