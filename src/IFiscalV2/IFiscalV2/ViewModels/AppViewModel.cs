using IFiscalV2.Mvvm;
using IFiscalV2.Services.Auth;
using IFiscalV2.Services.Routing;
using IFiscalV2.Views;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IFiscalV2.ViewModels
{
    public class AppViewModel : BaseViewModel
    {
        private readonly IRoutingService _routingService;
        private readonly IAuthService _authService;

        //TODO
        private bool isAdmin;
        public bool IsAdmin { get => isAdmin; set => SetProperty(ref isAdmin, value); }

        bool isStarting = true;
        public bool IsStarting { get => isStarting; set => SetProperty(ref isStarting, value); }

        public AppViewModel(IRoutingService routingService = null, IAuthService authService = null)
        {
            _instance = this;

            _routingService = routingService ?? Locator.Current.GetService<IRoutingService>();
            _authService = authService ?? Locator.Current.GetService<IAuthService>();

            //MessagingCenter.Subscribe<LoginPage>(this, "admin", (sender) =>
            //{
            //    IsAdmin = true;
            //});

            //MessagingCenter.Subscribe<LoginPage>(this, "user", (sender) =>
            //{
            //    IsAdmin = false;
            //});

        }

        #region Singleton
        private static AppViewModel _instance;
        public static AppViewModel Instance => _instance is null ? new AppViewModel() : _instance;
        #endregion

        public ICommand ExecuteLogout => new Command(async () =>
        {
            //Shell.Current.FlyoutIsPresented = false;
            //await Shell.Current.Navigation.PopToRootAsync();
            //await Shell.Current.Navigation.PopAsync();
            Shell.Current.FlyoutIsPresented = false;
            await _authService.LogoutAsync();
            await _routingService.NavigateToAsync("//main/login");
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
        });
    }
}
