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
    public class SiteChangeViewModel : BaseViewModel
    {
        private readonly IRoutingService _routingService;
        private readonly IAuthService _authService;
        private readonly IPageWorkflowServiceOptions _pageWorkflowServiceOptions;
        private readonly SiteService _siteService;

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

            TapCommand = new Command<SiteDto>(async (args) => await OnTappedAsync(args), (args) => !IsCommandExecuting);
        }

        #region Singleton
        private static SiteChangeViewModel _instance;
        public static SiteChangeViewModel Instance => _instance is null ? new SiteChangeViewModel() : _instance;
        #endregion

        public async Task LoadSitesAsync()
        {
            IsBusy = true;

            var apiResult = await _siteService.FindSitesAsync();

            if (apiResult.IsSuccess)
                Sites = new ObservableCollection<SiteDto>(apiResult.Result.Data);

            IsBusy = false;
        }

        //TODO Seguir con lo que es la Seleccion del SITE
        public async Task OnTappedAsync(SiteDto s)
        {
            IsCommandExecuting = true;
            await Task.Delay(1000);
            IsCommandExecuting = false;
        }



    } // SiteChangeViewModel

}
