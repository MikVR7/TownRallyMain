using AndroidServices;
using UnityEngine;
using UnityEngine.UI;

namespace TownRally
{
    internal class PanelRallyMap : APanel
    {
        [SerializeField] private Button btnGotoMap = null;
        [SerializeField] private Button btnGotoMypos = null;
        //[SerializeField] private Slider sliderZoom = null;
        //private static readonly float minMapZoom = 2f;
        //private static readonly float maxMapZoom = 20.999f;


        internal override void Init(PanelsHandler.PanelType panelType)
        {
            base.Init(panelType);
            this.btnGotoMap.onClick.AddListener(OnBtnGotoMap);
            this.btnGotoMypos.onClick.AddListener(OnBtnGotoMypos);
            //this.sliderZoom.onValueChanged.AddListener(OnSliderZoomChanged);
            //MapsHandler.EventOut_MapOnChangeZoom.AddListenerSingle(MapOnChangeZoom);
            
        }

        private void OnEnable()
        {
            MapsHandler.EventIn_DisplayMap.Invoke(true);
        }

        private void OnDisable()
        {
            MapsHandler.EventIn_DisplayMap.Invoke(false);
        }

        //private void OnSliderZoomChanged(float value)
        //{
        //    float zoom = ((1f-value) * (maxMapZoom - minMapZoom)) + minMapZoom;
        //        //float zoom = (value - 0f) * (maxMapZoom - minMapZoom) / (1f - 0f) + minMapZoom;
        //    MapsHandler.EventIn_SetMapZoom.Invoke(zoom);
        //}

        //private void MapOnChangeZoom(float zoom) {
        //    float newValue = 1f-((zoom - minMapZoom) / (maxMapZoom - minMapZoom));
        //    sliderZoom.value = newValue;
        //}

        private void OnBtnGotoMap()
        {
            MapsHandler.EventIn_SetMapPosition.Invoke(new AndroidBridge.Location() { latitude = 47.073863f, longitude = 15.440505f });
        }

        private void OnBtnGotoMypos()
        {
            MapsHandler.EventIn_SetMapCurrentGPSPosition.Invoke();
        }
    }
}
