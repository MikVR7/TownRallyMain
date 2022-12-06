/*
  Copyright 2017 Google Inc. All Rights Reserved.
  <p>
  Licensed under the Apache License, Version 2.0 (the "License");
  you may not use this file except in compliance with the License.
  You may obtain a copy of the License at
  <p>
  http://www.apache.org/licenses/LICENSE-2.0
  <p>
  Unless required by applicable law or agreed to in writing, software
  distributed under the License is distributed on an "AS IS" BASIS,
  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  See the License for the specific language governing permissions and
  limitations under the License.
 */

package com.abk.gps_forground.utils;


import com.google.android.gms.location.LocationRequest;
import com.google.android.gms.location.Priority;

import org.jetbrains.annotations.NotNull;

public class Utils {
    public static long minTime = 30000;
    public static float minDistance = 20f;
    public static double latitude = 0;
    public static double longitude = 0;

    @NotNull
    public static LocationRequest getLocationRequest() {
        //        request.setInterval(10000);
//        request.setFastestInterval(5000);
//        request.setPriority(LocationRequest.PRIORITY_HIGH_ACCURACY);
        if(minDistance==0f)
            return new LocationRequest.Builder(Priority.PRIORITY_HIGH_ACCURACY, minTime)
                        .build();
        return new LocationRequest.Builder(Priority.PRIORITY_HIGH_ACCURACY, minTime)
                .setMinUpdateDistanceMeters(minDistance)
                .build();
    }
}