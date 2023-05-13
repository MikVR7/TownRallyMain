using System.Collections.Generic;
using UnityEngine;

namespace TownRally
{
    internal class RalliesHandler
    {
        private static RalliesHandler Instance = null;
        internal static EventIn_LoadRalliesList EventIn_LoadRalliesList = new EventIn_LoadRalliesList();
        internal static EventIn_SetCurrentRally EventIn_SetCurrentRally = new EventIn_SetCurrentRally();
        internal static EventOut_RalliesLoadingDone EventOut_RalliesLoadingDone = new EventOut_RalliesLoadingDone();
        internal static EventOut_StationsLoadingDone EventOut_StationsLoadingDone = new EventOut_StationsLoadingDone();
        internal static EventOut_TasksLoadingDone EventOut_TasksLoadingDone = new EventOut_TasksLoadingDone();

        internal static readonly string NO_RALLY = "no_rally_running_243546";
        
        private Dictionary<string, Rally> rallies { get; set; } = new Dictionary<string, Rally>();
        private Dictionary<string, Station> stations { get; set; } = new Dictionary<string, Station> { };
        private Dictionary<string, RallyTask> tasks { get; set; } = new Dictionary<string, RallyTask>();
        private RallyCreator rallyCreator = new RallyCreator();

        internal static Dictionary<string, Rally> VarOut_GetRallies() { return Instance.rallies; }
        internal static Dictionary<string, Station> VarOut_GetStations() { return Instance.stations; }
        internal static Dictionary<string, RallyTask> VarOut_GetTasks() { return Instance.tasks; }
        internal static Rally VarOut_GetRallyByID(string rallyID) { return Instance.rallies[rallyID]; }
        internal static string VarOut_GetIDByRally(Rally rally) {
            foreach (string key in Instance.rallies.Keys)
            {
                if (Instance.rallies[key].Equals(rally))
                {
                    return key;
                }
            }
            return RalliesHandler.NO_RALLY;
        }

        private string currentRallyID { get; set; } = string.Empty;
        private int currentStationIndex { get; set; } = -1;
        private int currentTaskIndex { get; set; } = -1;

        internal static Rally VarOut_CurrentRally()
        {
            if (string.IsNullOrEmpty(Instance.currentRallyID)) { return new Rally() { Name = NO_RALLY }; }
            return Instance.rallies[Instance.currentRallyID];
        }

        internal static Station VarOut_CurrentStation()
        {
            return Instance.stations[string.Concat(
                Instance.currentRallyID, "_",
                Instance.currentStationIndex.ToString())];
        }
        internal static RallyTask VarOut_CurrentTask()
        {
            return Instance.tasks[string.Concat(
                Instance.currentRallyID, "_",
                Instance.currentStationIndex.ToString(), "_",
                Instance.currentTaskIndex.ToString())];
        }

        internal void Init()
        {
            Instance = this;
            this.rallyCreator.Init();
            EventIn_LoadRalliesList.AddListenerSingle(LoadRalliesList);
            EventIn_SetCurrentRally.AddListenerSingle(SetCurrentRally);
        }

        private void LoadRalliesList()
        {
            DatabaseHandler.EventInOut_LoadDBRalliesAll.Invoke(OnLoadingRalliesDone);
        }

        private void OnLoadingRalliesDone(Dictionary<string, Rally> response)
        {
            this.rallies = response;
            EventOut_RalliesLoadingDone.Invoke();
        }
        
        private void LoadStations()
        {
            DatabaseHandler.EventInOut_LoadDBRallyStations.Invoke(this.currentRallyID, OnLoadingStationsDone);
        }

        private void OnLoadingStationsDone(Dictionary<string, Station> response)
        {
            this.stations = response;
            EventOut_StationsLoadingDone.Invoke();
        }

        private void LoadTasks()
        {
            DatabaseHandler.EventInOut_LoadDBRallyStationTasks.Invoke(this.currentRallyID, this.currentStationIndex, OnLoadingTasksDone);
        }

        private void OnLoadingTasksDone(Dictionary<string, RallyTask> response)
        {
            this.tasks = response;
            EventOut_TasksLoadingDone.Invoke();
        }

        //private void OnLoadingRalliesDone(DataSnapshot dataSnapshot)
        //{
        //    this.rallies.Clear();
        //    foreach (DataSnapshot child in dataSnapshot.Children)
        //    {
        //        try
        //        {
        //            Rally rally = JsonConvert.DeserializeObject<Rally>(child.Value.ToString());
        //            this.rallies.Add(child.Key.ToString(), rally);
        //        }
        //        catch (JsonException e)
        //        {
        //            Debug.LogWarning(e.Message);
        //        }
        //    }
        //    this.responseLoadingRalliesDone.Invoke();
        //}

        private void SetCurrentRally(string rallyID)
        {
            this.currentRallyID = rallyID;
            LoadStations();
            //DatabaseHandler.EventInOut_LoadDBData.Invoke(
            //        DatabaseHandler.PATH_RALLIES_ROOT + DatabaseHandler.PATH_RALLIES_STATIONS,
            //        OnLoadingRalliesDone);
        }


        

        //private void CurrentTaskFinished()
        //{
        //    Task currentTask = VarOut_CurrentTask();
        //    if (currentTask.NextTasks.Length > 0)
        //    {
        //        this.currentTaskIndex = currentTask.NextTasks[0];
        //        // TODO: throw event that new task was started!
        //    }
        //    else
        //    {
        //        // TODO: does a new station needs to be started?
        //        // TODO: or is the rally finished?
        //    }
        //}

        //private void ContinueAtNextUnfinishedStation()
        //{
        //    Rally rally = Instance.rallies[Settings.GetStr(Settings.ValStr.RallyID)];
        //    // all stations finished from pool -> goto next station in list
        //    int notFinishedID = this.GetNotFinishedStationFromPool(rally.Stations);
        //    if (notFinishedID >= 0)
        //    {
        //        // there is a not finished station -> do that.
        //    }
        //    else
        //    {
        //        // all stations are finished -> display end-screen
        //    }
        //}

        //private int GetNotFinishedStationFromPool(Station[] stations)
        //{
        //    bool isAnyPossibleNotFinishedYet = false;
        //    Rally rally = Instance.rallies[Settings.GetStr(Settings.ValStr.RallyID)];
        //    for (int i = 0; i < stations.Length; i++)
        //    {
        //        Station station = stations[i];

        //        //for (int j = 0; j < rally.Stations.Length; j++)
        //        //{
        //        //    if (rally.Stations.ContainsKey(stationID) && !rally.Stations[stationID].IsFinished)
        //        //    {
        //        //        return stationID;
        //        //    }
        //        //}
        //    }
        //    return -1;
        //}

        ////private void OnSetCurrentRally(int rallyID)
        ////{
        ////    this.currentRallyID = rallyID;
        ////    EventOut_RallyChanged.Invoke(this.rallies[this.currentRallyID]);
        ////}
    }
}
