using System.Collections.Generic;
using UnityEngine;

namespace TownRally
{
    internal class PanelRally : APanel
    {
        [SerializeField] private Dictionary<RallyStationTask.Type, ARallyTask> rallyTasks = new Dictionary<RallyStationTask.Type, ARallyTask>(); 

        internal override void Init(PanelsHandler.PanelType panelType)
        {
            base.Init(panelType);
            foreach(RallyStationTask.Type task in this.rallyTasks.Keys)
            {
                this.rallyTasks[task].Init(task);
            }
            RalliesHandler.EventOut_RallyStationTaskChanged.AddListenerSingle(RallyStationTaskChanged);
        }

        internal override void SetActive(bool active)
        {
            base.SetActive(active);
            if (active) {
                RallyStationTaskChanged();
            }
        }

        private void RallyStationTaskChanged()
        {
            RallyStationTask currentTask = RalliesHandler.VarOut_GetCurrentTask();
            foreach (RallyStationTask.Type taskType in this.rallyTasks.Keys)
            {
                rallyTasks[taskType].SetActive(currentTask.TaskType.Equals(taskType));
            }
        }
    }
}
