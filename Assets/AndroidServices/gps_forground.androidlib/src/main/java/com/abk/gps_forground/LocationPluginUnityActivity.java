package com.abk.gps_forground;

import android.app.Fragment;
import android.app.FragmentManager;
import android.content.Intent;

import com.unity3d.player.UnityPlayerActivity;

public class LocationPluginUnityActivity extends UnityPlayerActivity {
    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        FragmentManager fragmentManager = getFragmentManager();
        Fragment fragmentByTag = fragmentManager.findFragmentByTag(LocationFragment.class.getName());
        if (fragmentByTag != null) {
            fragmentByTag.onActivityResult(requestCode, resultCode, data);
        }
    }
}
