using UnityEngine;

namespace TownRally
{
    internal abstract class ARallyTask : MonoBehaviour
    {
        public RallyStationTask.Type VarOut_TaskType { get; protected set; } = RallyStationTask.Type.None;

        internal virtual void Init(RallyStationTask.Type rallyTask)
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
