using UnityEngine;

namespace TownRally
{
    internal class TownRallyHandler : MonoBehaviour
    {
        internal static EventOut_OnUpdate EventOut_OnUpdate = new EventOut_OnUpdate();

        [SerializeField] private PanelsHandler panelsHandlerBody = null;
        [SerializeField] private TaskBarHandler taskBarHandler = null;
        private RalliesHandler ralliesHandler = new RalliesHandler();

        private void Awake()
        {
            this.ralliesHandler.Init();
            this.taskBarHandler.Init();
            this.panelsHandlerBody.Init();
        }

        private void Update()
        {
            EventOut_OnUpdate.Invoke();
        }
    }
}
