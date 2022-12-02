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

    public void stopForegroundService() {
        FragmentManager fragmentManager = activity.getFragmentManager();
        Fragment fragmentByTag = fragmentManager.findFragmentByTag(LocationFragment.class.getName());
        if (fragmentByTag != null) {
            ((LocationFragment) fragmentByTag).removeLocationUpdates();
            fragmentManager.beginTransaction().remove(fragmentByTag).commitAllowingStateLoss();
        }
    }

    public String getLocation() {
        if (Utils.latitude == 0 || Utils.longitude == 0)
            return "";
        JSONObject obj = new JSONObject();
        try {
            obj.put("lat", Utils.latitude);
            obj.put("long", Utils.longitude);
            Log.e("Location", "get Last LocationData: \n" + obj);
            return obj.toString();
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return "";
//        UnityPlayer.UnitySendMessage();
    }

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
