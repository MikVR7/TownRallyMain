using AndroidServices;
using GeoCoordinatePortable;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static AndroidServices.AndroidBridge;

namespace TownRally
{
    internal class MapDebugger : MonoBehaviour
    {
        [SerializeField] private OnlineMaps onlineMaps = null;
        [SerializeField] private TextMeshProUGUI tmpDebug = null;
        [SerializeField] private Button btnGotoCurrentPos = null;
        [SerializeField] private MapsHandler mapsHandler = null;
        private GeoCoordinate lastWaypoint = new GeoCoordinate() { Latitude = 0f, Longitude = 0f };
        private GeoCoordinate currentPosition = new GeoCoordinate();

        private float interval = 1f;
        private float nextTime = 0;
        private int waypointCount = 0;

        private void Awake()
        {
            StartCoroutine(this.StartLocationService());
            btnGotoCurrentPos.onClick.AddListener(OnBtnGotoCurrentPos);
            this.mapsHandler.Init(false);
        }

        private void OnBtnGotoCurrentPos()
        {
#if UNITY_EDITOR
            OnResponseGotoCurrentPos(new Location()
            {
                latitude = 47.06384f,
                longitude = 15.44817f,
                time = DateTime.UtcNow.ToLongDateString()
            });
#endif
            AndroidBridge.EventInOut_GetCurrentLocation.Invoke(OnResponseGotoCurrentPos);
            
        }

        private void OnResponseGotoCurrentPos(AndroidBridge.Location location) {
            this.tmpDebug.text = location.latitude + " " + location.longitude;
            
            this.currentPosition.Latitude = location.latitude;
            this.currentPosition.Longitude = location.longitude;

            this.tmpDebug.text = "UPDATE! " + count++;
            Debug.Log("TEST: " + count);
            this.UpdateMapView();
            this.CheckForNextWaypoint();
            this.CheckForNextStation();
        }

        private IEnumerator StartLocationService()
        {
            // Check if the user has location service enabled.
            if (!Input.location.isEnabledByUser)
            {
                currentPosition = GlobalConfig.GeoPosHome;
                yield break;
            }

            // Starts the location service.
            Input.location.Start();

            // Waits until the location service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            // If the service didn't initialize in 20 seconds this cancels location service use.
            if (maxWait < 1)
            {
                Debug.Log("Timed out");
                yield break;
            }

            // If the connection failed this cancels location service use.
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.Log("Unable to determine device location");
                yield break;
            }
            else
            {
                // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
                Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            }

            // Stops the location service if there is no need to query location updates continuously.
            Input.location.Stop();
        }

        private void Update()
        {
            return;

            if(Application.platform != RuntimePlatform.Android) {
                Debug.Log("NOT RUNNING!!" + Input.location.status);
                HandleKeyInputs();    
                return;
            }

            Debug.Log("NEXT TIME: " + (Time.time >= nextTime) + " " + Time.time + " " + nextTime);

            if (Time.time >= nextTime)
            {
                this.UpdateGeoPosition();
                
                nextTime += interval;
            }
        }

        private void HandleKeyInputs()
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                this.currentPosition.Latitude += 0.00002f;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.currentPosition.Latitude -= 0.00002f;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.currentPosition.Longitude -= 0.00002f;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.currentPosition.Longitude += 0.00002f;
            }

            this.UpdateMapView();
            this.CheckForNextWaypoint();
            this.CheckForNextStation();
        }

        private int count = 0;
        private void UpdateGeoPosition()
        {
            this.currentPosition.Latitude = Input.location.lastData.latitude;
            this.currentPosition.Longitude = Input.location.lastData.longitude;

            this.tmpDebug.text = "UPDATE! " + count++;
            Debug.Log("TEST: " + count);
            this.UpdateMapView();
            this.CheckForNextWaypoint();
            this.CheckForNextStation();
        }

        private void UpdateMapView() {
            this.onlineMaps.SetPosition(this.currentPosition.Longitude, this.currentPosition.Latitude);
            MapObjectsHandler.EventIn_SetMapObjectPosition.Invoke(this.currentPosition, GlobalConfig.MapObjectNameCharMain);
        }

        private void CheckForNextWaypoint()
        {
            float distance = this.GetMetricDistance(this.lastWaypoint, this.currentPosition);
            //Debug.Log("DISTANCE: " + distance + " " + lastWaypoint.Longitude + " " + lastWaypoint.Latitude + " | " + this.currentPosition.Longitude + " " + this.currentPosition.Latitude);
            if (distance > 30f)
            {
                //Debug.Log("NEXT WP! " + distance);
                this.lastWaypoint.Latitude = this.currentPosition.Latitude;
                this.lastWaypoint.Longitude = this.currentPosition.Longitude;

                MapObjectsHandler.EventIn_AddMapObject.Invoke(this.currentPosition, MapObject.Type.Waypoint, GlobalConfig.MapObjectNameWaypointSuffix + waypointCount++);
            }
        }

        private float GetMetricDistance(GeoCoordinate gc1, GeoCoordinate gc2)
        {
            return (float)gc2.GetDistanceTo(gc1);
        }

        private void CheckForNextStation()
        {
            //GetStations
            List<MapObject> stations = MapObjectsHandler.VarOut_GetStations();

            for(int i = 0; i< stations.Count; i++)
            {
                if (stations[i].stationDone == false)
                {
                    float distance = GetMetricDistance(stations[i].GetPosition(), this.currentPosition);
                    if(distance < 20f)
                    {
                        stations[i].stationDone = true;
                        stations[i].SetType(MapObject.Type.DestinationCheck);
                    }
                    //this.tmpDebug.text = "Distance to next station: " + distance.ToString("F2");
                }
            }
        }
    }
}
