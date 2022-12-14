using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TownRally
{
    internal class MapDebugger : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmpDebug = null;
        [SerializeField] private Button btnDebug = null;
        [SerializeField] private MapsHandler mapsHandler = null;
        
        private void Awake()
        {
            this.btnDebug.onClick.AddListener(this.OnBtnDebug);
            this.mapsHandler.Init(false);
        }

        private void OnBtnDebug()
        {

        }
    }
}
