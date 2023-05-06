
using UnityEngine;

namespace TownRally
{
    internal class TourEditorMain : MonoBehaviour
    {
        internal void Init()
        {
            TaskBarHandler.EventOut_OnBtnDebug.AddListenerSingle(OnBtnDebug);
        }

        private void OnBtnDebug()
        {
            MapsHandler.EventOut_OnMapClick.AddListenerSingle(OnMapClick);
        }

        private void OnMapClick(Vector2 position)
        {

        }
    }
}
