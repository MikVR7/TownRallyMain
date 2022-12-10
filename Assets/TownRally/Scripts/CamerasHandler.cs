using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace TownRally
{
    internal class CamerasHandler : MonoBehaviour
    {
        [SerializeField] private Camera camBase = null;
        [SerializeField] private Camera camUI = null;
        [SerializeField] private Camera camMap = null;

        internal void Init()
        {
            UniversalAdditionalCameraData cameraDataBase = camBase.GetUniversalAdditionalCameraData();
            //cameraDataBase.cameraStack.Add(camMap);
            cameraDataBase.cameraStack.Insert(0, camMap);
        }
    }
}
