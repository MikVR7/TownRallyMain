using Firebase;
using Firebase.Database;
using Firebase.Storage;
using Newtonsoft.Json;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;

namespace TownRally
{
    internal class DatabaseHandler : MonoBehaviour
    {
        internal static EventIn_SaveRallyWhole EventIn_SaveRallyWhole = new EventIn_SaveRallyWhole();
        internal static EventInOut_LoadDBRalliesAll EventInOut_LoadDBRalliesAll = new EventInOut_LoadDBRalliesAll();
        internal static EventInOut_LoadDBRallyStations EventInOut_LoadDBRallyStations = new EventInOut_LoadDBRallyStations();
        internal static EventInOut_LoadDBRallyStationTasks EventInOut_LoadDBRallyStationTasks = new EventInOut_LoadDBRallyStationTasks();

        internal static EventIn_SaveImage EventIn_SaveImage = new EventIn_SaveImage();
        internal static EventInOut_LoadImage EventInOut_LoadImage = new EventInOut_LoadImage();

        private static readonly string API_KEY = "AIzaSyAZtDvKrSEU7jCS1bzaGEalRX-NGELRAxQ";
        private static readonly string PROJECT_ID = "townrally-userbase"; // You can find this in your Firebase project settings
        private static readonly string DATABASE_URL = "https://townrally-userbase-default-rtdb.europe-west1.firebasedatabase.app/";
        //private static readonly string DATABASE_URL_STORAGE = "gs://townrally-userbase.appspot.com";

        private static readonly string APP_ID = "1:87340128502:android:c4096aaf2190a8b103f4d7";
        private static readonly string STORAGE_BUCKET = "townrally-userbase.appspot.com";// "com.Tokele.TownRallyUI";
        internal static readonly string PATH_RALLIES_ROOT = "rallies/";
        internal static readonly string PATH_RALLIES_RALLIES = "rallies/";
        internal static readonly string PATH_RALLIES_STATIONS = "stations/";
        internal static readonly string PATH_RALLIES_TASKS = "tasks/";
        //internal static readonly string PATH_STORAGE = "gs://townrally-userbase.appspot.com";

        private FirebaseDatabase firebaseDatabase = null;
        private FirebaseStorage firebaseStorage = null;
        StorageReference storageRef = null;

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
            this.firebaseStorage = FirebaseStorage.GetInstance(firebaseApp);
            
            //StorageReference storageRef = firebaseStorage.GetReferenceFromUrl(PATH_STORAGE);
            //StorageReference imageRef = firebaseStorage.Child("Rallies/SchlossbergRally/descr_00.jpg");


            EventIn_SaveImage.AddListenerSingle(SaveImage);
            EventInOut_LoadImage.AddListenerSingle(LoadImage);
            EventIn_SaveRallyWhole.AddListenerSingle(SaveRallyWhole);
            EventInOut_LoadDBRalliesAll.AddListenerSingle(LoadDBRalliesAll);
            EventInOut_LoadDBRallyStations.AddListenerSingle(LoadDBRallyStations);
            EventInOut_LoadDBRallyStationTasks.AddListenerSingle(LoadDBRallyStationTasks);
        }

        private async Task<bool> DeleteRallyWhole(string rallyID)
        {
            DatabaseReference reference = this.firebaseDatabase.GetReference(PATH_RALLIES_ROOT + PATH_RALLIES_RALLIES + rallyID);
            await reference.SetValueAsync(null);
            reference = this.firebaseDatabase.GetReference(PATH_RALLIES_ROOT + PATH_RALLIES_STATIONS + rallyID);
            await reference.SetValueAsync(null);

            bool tasksFound = true;
            int count = 0;
            while (tasksFound) {
                string taskName = rallyID + "_" + count++;
                if (await this.RallyTaskExists(taskName))
                {
                    reference = this.firebaseDatabase.GetReference(PATH_RALLIES_ROOT + PATH_RALLIES_TASKS + taskName);
                    await reference.SetValueAsync(null);
                }
                else
                {
                    tasksFound = false;
                }
            }
            return true;
        }

        private void SaveRallyWhole(string rallyID, Rally rally, Dictionary<string, Station> stations, Dictionary<string, RallyTask> tasks)
        {
            SaveRallyWholeAsync(rallyID, rally, stations, tasks);
        }

        private async void SaveRallyWholeAsync(string rallyID, Rally rally, Dictionary<string, Station> stations, Dictionary<string, RallyTask> tasks)
        {
            await DeleteRallyWhole(rallyID);

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
            _ = LoadDbData(PATH_RALLIES_ROOT + PATH_RALLIES_RALLIES, resultFunction);
        }
        private void LoadDBRallyStations(string rallyId, Action<Dictionary<string, Station>> resultFunction)
        {
            _ = LoadDbData(PATH_RALLIES_ROOT + PATH_RALLIES_STATIONS + "/" + rallyId, resultFunction);
        }
        private void LoadDBRallyStationTasks(string rallyId, int stationIndex, Action<Dictionary<string, RallyTask>> resultFunction)
        {
            _ = LoadDbData(PATH_RALLIES_ROOT + PATH_RALLIES_STATIONS + "/" + rallyId + "_" + stationIndex, resultFunction);
        }

        private async Task<bool> RallyExists(string rallyID)
        {
            Dictionary<string, RallyTask> rallyTask = await LoadDbData<RallyTask>(PATH_RALLIES_ROOT + PATH_RALLIES_RALLIES + "/" + rallyID, null);
            return rallyTask.Count > 0;
        }
        private async Task<bool> StationExists(string rallyID, string stationID)
        {
            Dictionary<string, RallyTask> rallyTask = await LoadDbData<RallyTask>(PATH_RALLIES_ROOT + PATH_RALLIES_STATIONS + "/" + rallyID + "/" + stationID, null);
            return rallyTask.Count > 0;
        }
        private async Task<bool> RallyTaskExists(string taskID)
        {
            Dictionary<string, RallyTask> rallyTask = await LoadDbData<RallyTask>(PATH_RALLIES_ROOT + PATH_RALLIES_TASKS + "/" + taskID, null);
            return rallyTask.Count > 0;
        }



        private async Task<Dictionary<string, T>> LoadDbData<T>(string path, Action<Dictionary<string, T>> resultFunction)
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
            if (resultFunction != null)
            {
                resultFunction.Invoke(results);
            }
            return results;
        }

        private void SaveImage(string path, Texture2D texture)
        {

        }

        //private async void LoadImage(string path, Action<Texture2D> onLoadImage)
        //{
        //    FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        //    //StorageReference storageRef = storage.GetReferenceFromUrl("gs://townrally-userbase.appspot.com");
        //    StorageReference storageRef = storage.GetReference("gs://com.Tokele.TownRallyUI.appspot.com");
        //    //StorageReference imageRef = storageRef.Child("Rallies/SchlossbergRally");
        //    //FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        //    //StorageReference storageRef = storage.RootReference;
        //    StorageReference imageRef = storageRef.Child("Rallies/SchlossbergRally/descr_00.jpg");
        //    //imageRef.Root = "gs://townrally-userbase.appspot.com";
        //    Debug.Log("DONE! " + imageRef.Path + " " + imageRef.Name + " " + imageRef.Bucket + " " + imageRef.Root);
        //    //return;
        //    await imageRef.GetBytesAsync(-1).ContinueWith(task => {
        //        if (task.IsFaulted || task.IsCanceled)
        //        {
        //            Debug.LogError(task.Exception.ToString());
        //        }
        //        else
        //        {
        //            Texture2D texture = new Texture2D(1, 1);
        //            if (texture.LoadImage(task.Result))
        //            {
        //                onLoadImage.Invoke(texture);
        //            }
        //        }
        //    });
        //}
        private async void LoadImage(string path, Action<Texture2D> onLoadImage)
        {
            try
            {
                // Create a reference to the file you want to download
                StorageReference fileReference = firebaseStorage.GetReference("Rallies/SchlossbergRally/descr_00.jpg");

                // Download the file to a new byte array
                byte[] fileContents = await fileReference.GetBytesAsync(long.MaxValue);

                Debug.Log("Finished downloading file.");

                // Convert the downloaded bytes to a Texture2D
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(fileContents);
                onLoadImage.Invoke(tex);
                Debug.Log("DONE TEXTURE... " + fileContents.Length + " " + tex.texelSize);
                // Use the tex Texture2D as you need
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                // Handle any errors
            }





            //// Create a reference to the file you want to download
            //StorageReference fileReference = firebaseStorage.GetReference("Rallies/SchlossbergRally/descr_00.jpg");

            //// Download the file to a new byte array
            ///*await */fileReference.GetBytesAsync(long.MaxValue).ContinueWith((Task<byte[]> task) => {
            //    if (task.IsFaulted || task.IsCanceled)
            //    {
            //        Debug.Log(task.Exception.ToString());
            //        // Handle any errors
            //    }
            //    else
            //    {
            //        byte[] fileContents = task.Result;
            //        Debug.Log("Finished downloading file.");
            //        // Convert the downloaded bytes to a Texture2D
            //        Texture2D tex = new Texture2D(2, 2);
            //        tex.LoadImage(fileContents);
            //        onLoadImage(tex);
            //                  Debug.Log("DONE SENDING TEXTURE!");
            //        // Use the fileContents byte array as you need
            //    }
            //});

        }
    }
}
