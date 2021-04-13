using IFiscalV2.Common;
using IFiscalV2.Mvvm;
using IFiscalV2.Services.Auth;
using IFiscalV2.Services.Routing;
using IFiscalV2.Views;
using Splat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IFiscalV2.ViewModels
{
    public class AppViewModel : BaseViewModel, IPageWorkflowServiceOptions
    {
        private readonly IRoutingService _routingService;
        private readonly IAuthService _authService;
        private readonly IPageWorkflowServiceOptions _pageWorkflowServiceOptions;

        #region VIEWMODELS for Locator
        public SiteChangeViewModel SelectSiteVM => Locator.Current.GetService<SiteChangeViewModel>();
        #endregion


        bool isStarting = true;
        public bool IsStarting { get => isStarting; set => SetProperty(ref isStarting, value); }

        public AppViewModel(
            IRoutingService routingService = null, IAuthService authService = null,
            IPageWorkflowServiceOptions pageWorkflowServiceOptions = null
            )
        {
            _instance = this;

            _routingService = routingService ?? Locator.Current.GetService<IRoutingService>();
            _authService = authService ?? Locator.Current.GetService<IAuthService>();
            _pageWorkflowServiceOptions = _pageWorkflowServiceOptions ?? Locator.Current.GetService<IPageWorkflowServiceOptions>();

            MessagingCenter.Subscribe<IShellUpdateMessage>(this, "shell_update", async (sender) => 
            {
                await ShellUpdate();
            });

        } // AppViewModel CTOR

        #region Singleton
        private static AppViewModel _instance;
        public static AppViewModel Instance => _instance is null ? new AppViewModel() : _instance;
        #endregion

        public ICommand ExecuteLogout => new Command(async () =>
        {
            Shell.Current.FlyoutIsPresented = false;
            await _authService.LogoutAsync();
            await _routingService.NavigateToAsync("//main/login");
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
        });


        private IPageWorkflowService _pageWorkflowService => (_pageWorkflowServiceOptions as IPageWorkflowService);

        private async Task ShellUpdate()
        {
            _pageWorkflowService.SincFromSettings();
            _pageWorkflowService.BuildShellVisibleItems();

            IsVisible_SiteChange = _pageWorkflowServiceOptions.IsVisible_SiteChange;
            IsVisible_EleccionChange = _pageWorkflowServiceOptions.IsVisible_EleccionChange;
            IsVisible_Resultados = _pageWorkflowServiceOptions.IsVisible_Resultados;
            IsVisible_Mesas = _pageWorkflowServiceOptions.IsVisible_Mesas;

            Route = _pageWorkflowService.GetNextRoute();

            //await Shell.Current.Navigation.PopToRootAsync(animated: false);
            await _routingService.NavigateToAsync(Route);


        } // ShellUpdate

        #region IPageWorkWorkflowOptions

        private bool inGlobal;
        public bool InGlobal { get => inGlobal; set => SetProperty(ref inGlobal, value); }

        private bool inEleccion;
        public string SelectedSiteId { get; set; }
        public bool InEleccion { get => inEleccion; set => SetProperty(ref inEleccion, value); }

        private string selectedSiteName;
        public string SelectedSiteName { get => selectedSiteName; set => SetProperty(ref selectedSiteName, value); }
        public string SelectedEleccionId { get; set; }
        private string selectedEleccionName;
        public string SelectedEleccionName { get => selectedEleccionName; set => SetProperty(ref selectedEleccionName, value); }

        public string Route { get; set; } = "//main";
        public bool BackButtonNoAllowed { get; set; } = false;

        private bool isVisible_SiteChange = false;
        public bool IsVisible_SiteChange { get => isVisible_SiteChange; set => SetProperty(ref isVisible_SiteChange, value); }

        private bool isVisible_EleccionChange = false;
        public bool IsVisible_EleccionChange { get => isVisible_EleccionChange; set => SetProperty(ref isVisible_EleccionChange, value); }

        private bool isVisible_Resultados = false;
        public bool IsVisible_Resultados { get => isVisible_Resultados; set => SetProperty(ref isVisible_Resultados, value); }

        private bool isVisible_Mesas = false;
        public bool IsVisible_Mesas { get => isVisible_Mesas; set => SetProperty(ref isVisible_Mesas, value); }

        #endregion



    } // AppViewModel
}
