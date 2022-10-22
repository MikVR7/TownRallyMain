using UnityEngine;

namespace TownRally
{
    internal class TownRallyHandler : MonoBehaviour
    {
        [SerializeField] private PanelsHandler panelsHandlerBody = null;
        [SerializeField] private TaskBarHandler taskBarHandler = null;
        private RalliesHandler ralliesHandler = new RalliesHandler();

        private void Awake()
        {
            this.ralliesHandler.Init();
            this.taskBarHandler.Init();
            this.panelsHandlerBody.Init();
        }
    }
}
