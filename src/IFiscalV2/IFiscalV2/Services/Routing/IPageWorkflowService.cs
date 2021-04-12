namespace IFiscalV2.Services.Routing
{
    public interface IPageWorkflowService
    {
        void SincFromSettings();
        void BuildShellVisibleItems();
        string GetNextRoute();
    }


}
