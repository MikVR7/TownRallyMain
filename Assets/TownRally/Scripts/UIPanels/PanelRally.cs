using System.Collections.Generic;
using UnityEngine;

namespace TownRally
{
    internal class PanelRally : APanel
    {
        [SerializeField] private Dictionary<Task.Type, ARallyTask> rallyTasks = new Dictionary<Task.Type, ARallyTask>(); 

        internal override void Init(PanelsHandler.PanelType panelType)
        {
            base.Init(panelType);
            foreach(Task.Type task in this.rallyTasks.Keys)
            {
                this.rallyTasks[task].Init(task);
            }
            //RalliesHandler.EventOut_RallyStationTaskChanged.AddListenerSingle(RallyStationTaskChanged);
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
            Task currentTask = RalliesHandler.VarOut_CurrentTask();
            foreach (Task.Type taskType in this.rallyTasks.Keys)
            {
                rallyTasks[taskType].SetActive(currentTask.TType.Equals(taskType));
            }
        }
    }
}
