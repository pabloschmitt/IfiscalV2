using System.Threading.Tasks;
using Xamarin.Forms;

namespace IFiscalV2.Services.Routing
{
    public class ShellRoutingService : IRoutingService
    {
        public ShellRoutingService()
        {
        }

        public Task NavigateToAsync(string route)
        {
            return Shell.Current.GoToAsync(route);
        }

        public Task GoBackAsync()
        {
            return Shell.Current.Navigation.PopAsync();
        }

        public Task GoBackModalAsync()
        {
            return Shell.Current.Navigation.PopModalAsync();
        }
    }
}
