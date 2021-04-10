namespace IFiscalV2.Infrastructure
{
    using IFiscalV2.ViewModels;
    using Splat;
    
    public class InstanceLocator
    {
        public AppViewModel Main { get; set; }

        public InstanceLocator()
        {
            Main = Main ?? Locator.Current.GetService<AppViewModel>();
        }

    }
}
