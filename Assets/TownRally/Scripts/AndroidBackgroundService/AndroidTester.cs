using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TownRally
{
    internal class AndroidTester : MonoBehaviour
    {
        [SerializeField] private BackgroundService backgroundService;
        [SerializeField] private Button btnStart = null;
        [SerializeField] private Button btnStop = null;
        [SerializeField] private Button btnSyncData = null;
        [SerializeField] private TextMeshProUGUI tmpDebug = null;

        private void Awake()
        {
            this.btnStart.onClick.AddListener(OnBtnStart);
            this.btnStop.onClick.AddListener(OnBtnStop);
            this.btnSyncData.onClick.AddListener(OnBtnSyncData);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                OnBtnStart();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                OnBtnStop();
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                OnBtnSyncData();
            }
        }

        private void OnBtnStart()
        {
            this.backgroundService.StartService();
        }

        private void OnBtnStop()
        {
            this.backgroundService.StopService();
        }
        private void OnBtnSyncData()
        {
            this.backgroundService.SyncData();
        }

    }
}
