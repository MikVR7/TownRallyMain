using UnityEngine;

namespace TownRally
{
    internal abstract class ARallyTask : MonoBehaviour
    {
        public RallyStationTask.RallyTask VarOut_TaskType { get; protected set; } = RallyStationTask.RallyTask.None;

        internal virtual void Init(RallyStationTask.RallyTask rallyTask)
        {
            this.VarOut_TaskType = rallyTask;
            this.SetActive(false);
        }

        internal virtual void SetActive(bool active)
        {
            this.gameObject.SetActive(active);
        }
    }
}
