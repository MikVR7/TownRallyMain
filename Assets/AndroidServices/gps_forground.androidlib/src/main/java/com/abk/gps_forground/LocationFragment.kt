@file:Suppress("DEPRECATION")

package com.abk.gps_forground

import android.Manifest
import android.app.Activity
import android.app.Fragment
import android.content.Intent
import android.content.IntentSender
import android.content.pm.PackageManager
import android.os.Bundle
import android.util.Log
import android.widget.Toast
import androidx.core.app.ActivityCompat
import com.abk.gps_forground.services.LocationService
import com.abk.gps_forground.utils.UnityCallbacks
import com.abk.gps_forground.utils.Utils
import com.google.android.gms.common.api.ApiException
import com.google.android.gms.common.api.ResolvableApiException
import com.google.android.gms.location.LocationServices
import com.google.android.gms.location.LocationSettingsRequest
import com.google.android.gms.location.LocationSettingsStatusCodes
import com.google.android.gms.location.SettingsClient
import com.karumi.dexter.Dexter
import com.karumi.dexter.MultiplePermissionsReport
import com.karumi.dexter.PermissionToken
import com.karumi.dexter.listener.PermissionRequest
import com.karumi.dexter.listener.multi.MultiplePermissionsListener


class LocationFragment : Fragment() {
    private val TAG: String = "LocationFragment"

    var mLocationService: LocationService = LocationService()
    lateinit var mServiceIntent: Intent
    private var mSettingsClient: SettingsClient? = null

    /**
     * Constant used in the location settings dialog.
     */
    private val REQUEST_CHECK_SETTINGS = 0x1


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        mSettingsClient = LocationServices.getSettingsClient(activity)


        if (checkPermissions()) {
            getLocation()

        } else {
            takePermission()
        }
    }

    private fun getLocation() {

        val mLocationRequest = Utils.getLocationRequest()
        val builder = LocationSettingsRequest.Builder()
        builder.addLocationRequest(mLocationRequest)
        val mLocationSettingsRequest = builder.build()
        mSettingsClient?.checkLocationSettings(mLocationSettingsRequest)
            ?.addOnFailureListener(activity) { e ->
                when ((e as ApiException).statusCode) {
                    LocationSettingsStatusCodes.RESOLUTION_REQUIRED -> {
                        Log.i(
                            TAG, "Location settings are not satisfied. Attempting to upgrade " +
                                    "location settings "
                        )
                        try {
                            val rae = e as ResolvableApiException
                            rae.startResolutionForResult(activity, REQUEST_CHECK_SETTINGS)


                        } catch (sie: IntentSender.SendIntentException) {
                            Log.i(TAG, "PendingIntent unable to execute request.")
                        }
                    }
                    LocationSettingsStatusCodes.SETTINGS_CHANGE_UNAVAILABLE -> {
                        val errorMessage =
                            "Location settings are inadequate, and cannot be" + "fixed here. Fix in Settings."
                        Log.e(TAG, errorMessage);
                        Toast.makeText(activity, errorMessage, Toast.LENGTH_LONG).show();
                        //  mRequestingLocationUpdates = false;
                    }
                }
            }?.addOnSuccessListener {
                mServiceIntent = Intent(activity, mLocationService.javaClass)
                activity.startService(mServiceIntent)
            }
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)
        Log.e("LocationFragment", "onActivityResult: ")
        when (requestCode) {
            REQUEST_CHECK_SETTINGS -> when (resultCode) {
                Activity.RESULT_OK -> {
                    Log.i(TAG, "User agreed to make required location settings changes.")
                    mServiceIntent = Intent(activity, mLocationService.javaClass)
                    activity.startService(mServiceIntent)
                }
                Activity.RESULT_CANCELED -> {
                    UnityCallbacks.gpsDenied()
                    Log.i(
                        TAG,
                        "User chose not to make required location settings changes."
                    )

                }
            }
        }
    }

    private fun takePermission() {
        Dexter.withContext(activity)
            .withPermissions(
                Manifest.permission.ACCESS_FINE_LOCATION,
                Manifest.permission.ACCESS_COARSE_LOCATION
            )
            .withListener(object : MultiplePermissionsListener {

                override fun onPermissionRationaleShouldBeShown(
                    p0: MutableList<PermissionRequest>?,
                    p1: PermissionToken?
                ) {
                    p1?.continuePermissionRequest()
                }

                override fun onPermissionsChecked(p0: MultiplePermissionsReport) {
                    if (p0.areAllPermissionsGranted()) {
                        getLocation()
                    } else {

                        UnityCallbacks.permissionDenied("location")

                        stopLocationUpdates()
                    }
                }
            })
            .check()
    }

    fun stopLocationUpdates() {
        val fragmentManager = activity.fragmentManager
        val fragmentByTag =
            fragmentManager.findFragmentByTag(LocationFragment::class.java.name)
        if (fragmentByTag != null) {
            (fragmentByTag as LocationFragment).removeLocationUpdates()
            fragmentManager.beginTransaction().remove(fragmentByTag).commitAllowingStateLoss()
        }
    }

    /**
     * Returns the current state of the permissions needed.
     */
    private fun checkPermissions(): Boolean {
        return PackageManager.PERMISSION_GRANTED == ActivityCompat.checkSelfPermission(
            activity,
            Manifest.permission.ACCESS_FINE_LOCATION
        ) && PackageManager.PERMISSION_GRANTED == ActivityCompat.checkSelfPermission(
            activity,
            Manifest.permission.ACCESS_COARSE_LOCATION
        )
    }

    fun removeLocationUpdates() {
        if (::mServiceIntent.isInitialized) {
            mLocationService.removeLocationUpdates()
            activity.stopService(mServiceIntent)
        }
    }

    override fun onStop() {
        super.onStop()
    }

    override fun onDestroy() {
        if (::mServiceIntent.isInitialized) {
            activity.stopService(mServiceIntent)
        }
        super.onDestroy()
    }
}