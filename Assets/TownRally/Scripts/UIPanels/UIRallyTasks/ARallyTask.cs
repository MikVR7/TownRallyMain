using UnityEngine;

namespace TownRally
{
    internal abstract class ARallyTask : MonoBehaviour
    {
        public Task.Type VarOut_TaskType { get; protected set; } = Task.Type.None;

        internal virtual void Init(Task.Type rallyTask)
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
