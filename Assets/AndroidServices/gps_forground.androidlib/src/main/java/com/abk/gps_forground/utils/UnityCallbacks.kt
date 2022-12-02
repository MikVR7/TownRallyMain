package com.abk.gps_forground.utils

import com.unity3d.player.UnityPlayer

class UnityCallbacks {
    private val TAG: String = "UnityCallbacks"

    companion object {
        const val CALLBACK_OBJ: String = "LocationPluginListener"
        const val onLocationRetrieved: String = "onLocationRetrieved"
        const val onPermissionDenied: String = "onPermissionDenied"
        const val onGPSDenied: String = "onGPSDenied"


        fun locationDataRetrieved(response: String) {
            try {
                UnityPlayer.UnitySendMessage(CALLBACK_OBJ, onLocationRetrieved, response)
            } catch (e: Exception) {
                e.printStackTrace()
            }
        }

        fun permissionDenied(permission: String) {
            try {
                UnityPlayer.UnitySendMessage(CALLBACK_OBJ, onPermissionDenied, permission)
            } catch (e: Exception) {
                e.printStackTrace()
            }
        }

        fun gpsDenied() {
            try {
                UnityPlayer.UnitySendMessage(CALLBACK_OBJ, onGPSDenied, "GPS denied by user")
            } catch (e: Exception) {
                e.printStackTrace()
            }
        }

    }
}