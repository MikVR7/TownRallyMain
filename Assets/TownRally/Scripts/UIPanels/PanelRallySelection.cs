using System.Collections.Generic;
using UnityEngine;
using static TownRally.PanelsHandler;

namespace TownRally
{
    internal class PanelRallySelection : APanel
    {
        [SerializeField] private GameObject prefabLineRally = null;
        [SerializeField] private RectTransform rtContentHolder = null;

        private Dictionary<string, PanelRallySelectionLine> rallyLines = new Dictionary<string, PanelRallySelectionLine>();

        internal override void Init(PanelType panelType)
        {
            base.Init(panelType);
        }

        private void OnEnable()
        {
            Dictionary<string, Rally> rallies = RalliesHandler.VarOut_GetRallies();
            foreach (string rallyID in rallies.Keys)
            {
                if (!rallyLines.ContainsKey(rallyID))
                {
                    CreateLineRally(rallyID, rallies[rallyID]);
                }
            }
        }

        private void CreateLineRally(string rallyID, Rally rally)
        {
            GameObject goLineRally = Instantiate(prefabLineRally);
            goLineRally.name = "rally_" + name;
            RectTransform rtLineRally = goLineRally.GetComponent<RectTransform>();
            rtLineRally.SetParent(rtContentHolder);
            rtLineRally.localScale = new Vector3(1f, 1f, 1f);
            rtLineRally.localPosition = new Vector3(rtLineRally.localPosition.x, rtLineRally.localPosition.y, 0f);
            PanelRallySelectionLine lineRally = goLineRally.GetComponent<PanelRallySelectionLine>();
            lineRally.Init(rallyID, rally);
            this.rallyLines.Add(rallyID, lineRally);
        }
    }
}