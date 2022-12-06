package com.abk.gps_forground;

import static com.abk.gps_forground.utils.FileHelperKt.getAllLocationData;

import android.app.Activity;
import android.app.Fragment;
import android.app.FragmentManager;
import android.util.Log;

import com.abk.gps_forground.utils.FileHelperKt;
import com.abk.gps_forground.utils.UnityCallbacks;
import com.abk.gps_forground.utils.Utils;

import org.json.JSONException;
import org.json.JSONObject;


import android.location.Location;
//import android.location.LocationListener;
import android.location.LocationManager;
import android.content.Context;


public class LocationPlugin {
    Activity activity;

    public LocationPlugin(Activity activity) {
        this.activity = activity;
    }

    public void setMinDistance(float minDistance) {
        Utils.minDistance = minDistance;
    }

    public void setMinFetchTime(long minTime) {
        Utils.minTime = minTime;
    }

    public void startForegroundService() {
        FragmentManager fragmentManager = activity.getFragmentManager();
        if (fragmentManager.findFragmentByTag(LocationFragment.class.getName()) != null) {
            fragmentManager.beginTransaction().remove(fragmentManager.findFragmentByTag(LocationFragment.class.getName())).commitAllowingStateLoss();
        }
        fragmentManager.beginTransaction().add(new LocationFragment(), LocationFragment.class.getName()).commitAllowingStateLoss();
    }

    private LocationManager locationManager;
    //private LocationListener listener;
    public String locationData = "no_data";
    Location gps_loc;
    Location network_loc;
    public void onCreate(Context context) {
        locationManager = (LocationManager) context.getSystemService(Context.LOCATION_SERVICE);
    }

    public String GetCurrentLocation() {
        try {
            double lat = 0;
            double lon = 0;
            Log.d("D44", "DONE!");
            network_loc = locationManager.getLastKnownLocation(LocationManager.NETWORK_PROVIDER);
            if(network_loc != null) {
                Log.d("D4", "OK LOC GOT gps!");
                lat = network_loc.getLatitude();
                lon = network_loc.getLongitude();
            }
            else {
                gps_loc = locationManager.getLastKnownLocation(LocationManager.GPS_PROVIDER);
                if(network_loc != null) {
                    Log.d("D4", "OK LOC GOT network!");
                    lat = network_loc.getLatitude();
                    lon = network_loc.getLongitude();
                }
            }
            JSONObject obj = new JSONObject();
            obj.put("lat", lat);
            obj.put("long", lon);
            return obj.toString();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return "";
    }

    public void stopForegroundService() {
        FragmentManager fragmentManager = activity.getFragmentManager();
        Fragment fragmentByTag = fragmentManager.findFragmentByTag(LocationFragment.class.getName());
        if (fragmentByTag != null) {
            ((LocationFragment) fragmentByTag).removeLocationUpdates();
            fragmentManager.beginTransaction().remove(fragmentByTag).commitAllowingStateLoss();
        }
    }

    /*public String getLocation() {
        if (Utils.latitude == 0 || Utils.longitude == 0)
            return "";
        JSONObject obj = new JSONObject();
        try {
            obj.put("lat", Utils.latitude);
            obj.put("long", Utils.longitude);
            //obj.put("time", Utils.time);
            Log.e("Location", "get Last LocationData: \n" + obj);
            return obj.toString();
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return "";
//        UnityPlayer.UnitySendMessage();
    }*/

    public void getCoordinatesList() {
        String locationData = getAllLocationData(activity);
        Log.e("Location", "getLocationData: \n" + locationData);
        UnityCallbacks.Companion.locationDataRetrieved(locationData);
//        UnityPlayer.UnitySendMessage();
    }

    public void clearCoordinatesList() {
        FileHelperKt.clearAllData(activity);
    }
}
