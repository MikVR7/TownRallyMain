using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace TownRally
{
    internal class PanelRally : APanel
    {
        [SerializeField] private Dictionary<ARallyTask.RallyTask, ARallyTask> rallyTasks = new Dictionary<ARallyTask.RallyTask, ARallyTask>(); 

        internal override void Init(PanelsHandler.PanelType panelType)
        {
            base.Init(panelType);
            foreach(ARallyTask.RallyTask task in this.rallyTasks.Keys)
            {
                this.rallyTasks[task].Init(task);
            }
        }

        internal override void SetActive(bool active)
        {
            base.SetActive(active);
            RalliesHandler.VarOut_GetCurrentRally().Stations
        }
    }
}
