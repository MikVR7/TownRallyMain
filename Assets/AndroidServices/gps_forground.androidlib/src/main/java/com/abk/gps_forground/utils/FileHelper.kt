package com.abk.gps_forground.utils

import android.content.Context
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import org.json.JSONArray
import org.json.JSONObject
import java.io.File

const val FILE_NAME = "record.txt"

fun getAllLocationData(context: Context): String {
    val finalJSON = JSONObject()
    try {
        val file = File(context.filesDir, FILE_NAME)
        val locationArray = JSONArray()
        if (file.exists()) {
            file.forEachLine {
                val array = it.split(",")
                val json = JSONObject().apply {
                    put("time", array[0])
                    put("lat", array[1])
                    put("long", array[2])
                }
                locationArray.put(json)
            }
        }

        finalJSON.put("location", locationArray)

    } catch (e: Exception) {
        e.printStackTrace()
        finalJSON.put("location", JSONArray())
    }
    return finalJSON.toString()
}

fun writeDataToFile(
    context: Context,
    latitude: Double,
    longitude: Double,
    //time: Long,
) {

    val file = File(context.filesDir, FILE_NAME)
    if (!file.exists()) {
        file.createNewFile()
    } else {
        file.appendText("\n")
    }
    file.appendText("${System.currentTimeMillis()},$latitude,$longitude")
}

fun clearAllData(context: Context) {
    CoroutineScope(Dispatchers.IO).launch {
        try {
            val file = File(context.filesDir, FILE_NAME)
            if (file.exists())
                file.delete()
        } catch (e: Exception) {
            e.printStackTrace()
        }
    }
}