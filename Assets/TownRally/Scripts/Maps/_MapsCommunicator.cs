//using System.Collections;
//using UnityEngine;
//using UnityEngine.Rendering.Universal;
//using UnityEngine.SceneManagement;

//namespace TownRally
//{
//    internal class MapsCommunicator : MonoBehaviour
//    {
//        internal static EventIn_OpenCloseMapsScene EventIn_OpenCloseMapsScene = new EventIn_OpenCloseMapsScene();
//        internal bool isMapsOpen = false;
//        private MapsHandler mapsHandler = null;
//        [SerializeField] private UniversalAdditionalCameraData camData = null;
//        //[SerializeField] private 

//        internal void Init()
//        {
//            EventIn_OpenCloseMapsScene.AddListenerSingle(OpenCloseMapsScene);
//        }

//        private void OpenCloseMapsScene(bool openClose)
//        {
//            if (openClose && !this.isMapsOpen)
//            {
//                SceneManager.LoadSceneAsync(GlobalConfig.SceneNameMap, LoadSceneMode.Additive);
//                this.isMapsOpen = true;
//                StartCoroutine(GetMapshandler());
//            }
//            else if (!openClose && this.isMapsOpen)
//            {
//                SceneManager.UnloadSceneAsync(GlobalConfig.SceneNameMap);
//                this.isMapsOpen = false;
//                this.mapsHandler = null;
//            }
//        }

//        private IEnumerator GetMapshandler()
//        {
//            yield return new WaitForEndOfFrame();
//            yield return new WaitForEndOfFrame();
//            yield return new WaitForEndOfFrame();
//            yield return new WaitForEndOfFrame();
//            yield return new WaitForEndOfFrame();
//            this.mapsHandler = MapsHandler.Instance;
//            this.mapsHandler.Init();

//            this.camData.cameraStack.Add(this.mapsHandler.VarOut_Camera());
//        }
//    }
//}
