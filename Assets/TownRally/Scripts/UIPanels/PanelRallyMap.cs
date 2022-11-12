namespace TownRally
{
    internal class PanelRallyMap : APanel
    {
        internal override void Init(PanelsHandler.PanelType panelType)
        {
            base.Init(panelType);
        }

        private void OnEnable()
        {
            MapsCommunicator.EventIn_OpenCloseMapsScene.Invoke(true);
        }

        private void OnDisable()
        {
            MapsCommunicator.EventIn_OpenCloseMapsScene.Invoke(false);
        }
    }
}
