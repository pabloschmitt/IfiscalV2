using IFiscalV2.Mvvm;
using IFiscalV2.Services.Auth;
using IFiscalV2.Services.Routing;
using Splat;
using System.Threading.Tasks;

namespace IFiscalV2.ViewModels
{
    public class LoadingViewModel : BaseViewModel
    {
        private readonly IRoutingService _routingService;
        private readonly IAuthService _authService;
        private readonly AppViewModel _appViewModel;
        public LoadingViewModel(IRoutingService routingService = null, IAuthService authService = null)
        {
            _routingService = routingService ?? Locator.Current.GetService<IRoutingService>();
            _authService = authService ?? Locator.Current.GetService<IAuthService>();

            _appViewModel = Locator.Current.GetService<AppViewModel>();
        }

        // Called by the views OnAppearing method
        public async void Init()
        {
            var isAuthenticated = await _authService.CheckPreviusLoginAsync();

            await Task.Delay(150);

            if (isAuthenticated.IsSuccess)
            {
                await _routingService.NavigateToAsync("//main");
            }
            else
            {
                await _routingService.NavigateToAsync("//main/login");
            }
        }

    } // LoadingViewModel
}
