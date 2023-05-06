using Firebase;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TownRally
{
    internal class DatabaseHandler
    {
        //internal static EventInOut_LoadAllRallies EventInOut_LoadAllRallies = new EventInOut_LoadAllRallies();
        internal static EventIn_SaveRallyWhole EventIn_SaveRallyWhole = new EventIn_SaveRallyWhole();
        internal static EventInOut_LoadDBRalliesAll EventInOut_LoadDBRalliesAll = new EventInOut_LoadDBRalliesAll();
        internal static EventInOut_LoadDBRallyStations EventInOut_LoadDBRallyStations = new EventInOut_LoadDBRallyStations();
        internal static EventInOut_LoadDBRallyStationTasks EventInOut_LoadDBRallyStationTasks = new EventInOut_LoadDBRallyStationTasks();

        internal static EventIn_SaveImage EventIn_SaveImage = new EventIn_SaveImage();
        internal static EventInOut_LoadImage EventInOut_LoadImage = new EventInOut_LoadImage();

        private static readonly string API_KEY = "AIzaSyAZtDvKrSEU7jCS1bzaGEalRX-NGELRAxQ";
        private static readonly string PROJECT_ID = "townrally-userbase"; // You can find this in your Firebase project settings
        //private static readonly string databaseURL = $"https://{projectId}.firebaseio.com/";
        private static readonly string DATABASE_URL = "https://townrally-userbase-default-rtdb.europe-west1.firebasedatabase.app/";
        private static readonly string APP_ID = "1:87340128502:android:c4096aaf2190a8b103f4d7";
        private static readonly string STORAGE_BUCKET = "com.Tokele.TownRallyUI";
        internal static readonly string PATH_RALLIES_ROOT = "rallies/";
        internal static readonly string PATH_RALLIES_RALLIES = "rallies/";
        internal static readonly string PATH_RALLIES_STATIONS = "stations/";
        internal static readonly string PATH_RALLIES_TASKS = "tasks/";

        private FirebaseDatabase firebaseDatabase = null;

        internal void Init()
        {
            Uri uri = new Uri(DATABASE_URL);
            FirebaseApp firebaseApp = FirebaseApp.Create(new AppOptions()
            {
                ApiKey = API_KEY,
                AppId = APP_ID,
                ProjectId = PROJECT_ID,
                DatabaseUrl = uri,
                StorageBucket = STORAGE_BUCKET,
            });

            this.firebaseDatabase = FirebaseDatabase.GetInstance(firebaseApp);

            EventIn_SaveImage.AddListenerSingle(SaveImage);
            EventInOut_LoadImage.AddListenerSingle(LoadImage);
            //EventInOut_LoadAllRallies.AddListenerSingle(LoadAllRallies);
            EventIn_SaveRallyWhole.AddListenerSingle(SaveRallyWhole);
            EventInOut_LoadDBRalliesAll.AddListenerSingle(LoadDBRalliesAll);
            EventInOut_LoadDBRallyStations.AddListenerSingle(LoadDBRallyStations);
            EventInOut_LoadDBRallyStationTasks.AddListenerSingle(LoadDBRallyStationTasks);
        }

        public async void SaveRallyWhole(string rallyID, Rally rally, Dictionary<string, Station> stations, Dictionary<string, Task> tasks)
        {
            string dataStr = JsonConvert.SerializeObject(rally);
            DatabaseReference reference = this.firebaseDatabase.GetReference(PATH_RALLIES_ROOT + PATH_RALLIES_RALLIES + rallyID);
            await reference.SetValueAsync(dataStr);
            foreach (string stationID in stations.Keys)
            {
                dataStr = JsonConvert.SerializeObject(stations[stationID]);
                reference = this.firebaseDatabase.GetReference(PATH_RALLIES_ROOT + PATH_RALLIES_STATIONS + stations[stationID].RallyID + "/" + stationID);
                await reference.SetValueAsync(dataStr);
            }
            foreach (string taskID in tasks.Keys)
            {
                dataStr = JsonConvert.SerializeObject(tasks[taskID]);
                reference = this.firebaseDatabase.GetReference(PATH_RALLIES_ROOT + PATH_RALLIES_TASKS + tasks[taskID].StationID + "/" + taskID);
                await reference.SetValueAsync(dataStr);
            }
        }

        private void LoadDBRalliesAll(Action<Dictionary<string, Rally>> resultFunction)
        {
            LoadDbData(PATH_RALLIES_ROOT + PATH_RALLIES_RALLIES, resultFunction);
        }
        private void LoadDBRallyStations(string rallyId, Action<Dictionary<string, Station>> resultFunction)
        {
            LoadDbData(PATH_RALLIES_ROOT + PATH_RALLIES_STATIONS + "/" + rallyId, resultFunction);
        }
        private void LoadDBRallyStationTasks(string rallyId, int stationIndex, Action<Dictionary<string, Task>> resultFunction)
        {
            LoadDbData(PATH_RALLIES_ROOT + PATH_RALLIES_STATIONS + "/" + rallyId + "_" + stationIndex, resultFunction);
        }


        private async void LoadDbData<T>(string path, Action<Dictionary<string, T>> resultFunction)
        {
            DataSnapshot dataSnapshot = await this.firebaseDatabase.RootReference.Child(path).GetValueAsync();
            Dictionary<string, T> results = new Dictionary<string, T>();
            foreach (DataSnapshot child in dataSnapshot.Children)
            {
                try
                {
                    T result = JsonConvert.DeserializeObject<T>(child.Value.ToString());
                    results.Add(child.Key.ToString(), result);
                }
                catch (JsonException e)
                {
                    Debug.LogWarning(e.Message);
                }
            }
            resultFunction.Invoke(results);
        }

        private void SaveImage(string path, Texture2D texture)
        {

        }

        //private async void LoadAllRallies(Action<Dictionary<string, Rally>> onLoadRallies)
        //{
        //    DataSnapshot dataSnapshot = await this.firebaseDatabase.RootReference.Child("rallies").GetValueAsync();
        //    Dictionary<string, Rally> rallies = new Dictionary<string, Rally>();
        //    foreach (var child in dataSnapshot.Children)
        //    {
        //        try
        //        {
        //            Rally rally = JsonConvert.DeserializeObject<Rally>(child.GetRawJsonValue());
        //            rallies.Add(child.Key.ToString(), rally);
        //        }
        //            catch (JsonException e)
        //        {
        //            Debug.LogWarning(e.Message);
        //        }
        //    }
        //    onLoadRallies.Invoke(rallies);
        //}

        private void LoadImage(string path, Action<Texture2D> onLoadImage)
        {

        }
    }
}
