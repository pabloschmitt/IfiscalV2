using IFiscalV2.Models.Dto;
using IFiscalV2.Mvvm;
using IFiscalV2.Services.Auth;
using IFiscalV2.Services.Data;
using IFiscalV2.Services.Routing;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IFiscalV2.ViewModels
{
    public class SiteChangeViewModel : BaseViewModel, IShellUpdateMessage
    {
        private readonly IRoutingService _routingService;
        private readonly IAuthService _authService;
        private readonly IPageWorkflowServiceOptions _pageWorkflowServiceOptions;
        private readonly SiteService _siteService;

        private IPageWorkflowService _pageWorkflowService => (_pageWorkflowServiceOptions as IPageWorkflowService);

        private ObservableCollection<SiteDto> sites;

        public ObservableCollection<SiteDto> Sites
        {
            get { return sites; }
            set { SetProperty(ref sites, value); }
        }


        public ICommand TapCommand { get; private set; }

        public SiteChangeViewModel(
            IRoutingService routingService = null, IAuthService authService = null,
            IPageWorkflowServiceOptions pageWorkflowServiceOptions = null,
            SiteService siteService = null
            )
        {
            _instance = this;

            _routingService = routingService ?? Locator.Current.GetService<IRoutingService>();
            _authService = authService ?? Locator.Current.GetService<IAuthService>();
            _pageWorkflowServiceOptions = _pageWorkflowServiceOptions ?? Locator.Current.GetService<IPageWorkflowServiceOptions>();
            _siteService = siteService ?? Locator.Current.GetService<SiteService>();

            TapCommand = new Command<SiteDto>( (args) => OnTapped(args), (args) => !IsCommandExecuting);
        }

        #region Singleton
        private static SiteChangeViewModel _instance;
        public static SiteChangeViewModel Instance => _instance is null ? new SiteChangeViewModel() : _instance;
        #endregion

        public async Task LoadAsync()
        {
            IsBusy = true;

            var apiResult = await _siteService.FindSitesAsync();

            if (apiResult.IsSuccess)
                Sites = new ObservableCollection<SiteDto>(apiResult.Result.Data);

            IsBusy = false;
        }

        //TODO Seguir con lo que es la Seleccion del SITE
        public void OnTapped(SiteDto o)
        {
            IsCommandExecuting = true;

            _pageWorkflowServiceOptions.SelectedSiteId = o.Id;
            _pageWorkflowServiceOptions.SelectedSiteName = o.NormalizedName;
            _pageWorkflowServiceOptions.SelectedEleccionId = string.Empty;
            _pageWorkflowServiceOptions.SelectedEleccionName = string.Empty;
            _pageWorkflowServiceOptions.InGlobal = false;
            _pageWorkflowServiceOptions.InEleccion = false;
            _pageWorkflowService.Save();

            IsCommandExecuting = false;

            MessagingCenter.Send(this as IShellUpdateMessage, "shell_update");
            
        }



    } // SiteChangeViewModel

}
