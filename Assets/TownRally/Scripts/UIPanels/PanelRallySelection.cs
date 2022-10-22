using System.Collections.Generic;
using UnityEngine;

namespace TownRally
{
    internal class PanelRallySelection : MonoBehaviour
    {
        [SerializeField] private GameObject prefabLineRally = null;
        [SerializeField] private RectTransform rtContentHolder = null;

        private List<LineRally> rallyLines = new List<LineRally>();

        internal void Init()
        {
            this.rallyLines.Clear();
            for(int i = 0; i < RalliesHandler.VarOut_GetRalliesCount(); i++)
            {
                CreateLineRally(i, RalliesHandler.VarOut_GetRallyNameByID(i), RalliesHandler.VarOut_GetRallyCaptionByID(i));
            }
        }

        private void CreateLineRally(int id, string name, string caption)
        {
            GameObject goLineRally = Instantiate(prefabLineRally);
            goLineRally.name = "rally_" + name;
            RectTransform rtLineRally = goLineRally.GetComponent<RectTransform>();
            rtLineRally.SetParent(rtContentHolder);
            rtLineRally.localScale = new Vector3(1f, 1f, 1f);
            rtLineRally.localPosition = new Vector3(rtLineRally.localPosition.x, rtLineRally.localPosition.y, 0f);
            LineRally lineRally = goLineRally.GetComponent<LineRally>();
            lineRally.Init(id, name, caption);
            this.rallyLines.Add(lineRally);
        }
    }
}