using IFiscalV2.Common;
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
    public class EleccionChangeViewModel : BaseViewModel, IShellUpdateMessage
    {
        private readonly IRoutingService _routingService;
        private readonly IAuthService _authService;
        private readonly IPageWorkflowServiceOptions _pageWorkflowServiceOptions;
        private readonly EleccionService _eleccionService;

        private IPageWorkflowService _pageWorkflowService => (_pageWorkflowServiceOptions as IPageWorkflowService);

        private ObservableCollection<EleccionDto> elecciones;

        public ObservableCollection<EleccionDto> Elecciones
        {
            get { return elecciones; }
            set { SetProperty(ref elecciones, value); }
        }

        public ICommand TapCommand { get; private set; }

        public EleccionChangeViewModel(
            IRoutingService routingService = null, IAuthService authService = null,
            IPageWorkflowServiceOptions pageWorkflowServiceOptions = null,
            EleccionService eleccionService = null
            )
        {
            _instance = this;

            _routingService = routingService ?? Locator.Current.GetService<IRoutingService>();
            _authService = authService ?? Locator.Current.GetService<IAuthService>();
            _pageWorkflowServiceOptions = _pageWorkflowServiceOptions ?? Locator.Current.GetService<IPageWorkflowServiceOptions>();
            _eleccionService = eleccionService ?? Locator.Current.GetService<EleccionService>();

            TapCommand = new Command<EleccionDto>((args) => OnTapped(args), (args) => !IsCommandExecuting);
        }

        #region Singleton
        private static EleccionChangeViewModel _instance;
        public static EleccionChangeViewModel Instance => _instance is null ? new EleccionChangeViewModel() : _instance;
        #endregion

        public async Task LoadAsync()
        {

            var apiResult = await _eleccionService.FindAsync(_pageWorkflowServiceOptions.SelectedSiteId);

            if (apiResult.IsSuccess)
                Elecciones = new ObservableCollection<EleccionDto>(apiResult.Result.Data);

        }

        public void OnTapped(EleccionDto o)
        {
            IsCommandExecuting = true;

            _pageWorkflowServiceOptions.SelectedEleccionId = o.Id;
            _pageWorkflowServiceOptions.SelectedEleccionName = o.Name;
            _pageWorkflowServiceOptions.InEleccion = true;
            _pageWorkflowService.Save();

            IsCommandExecuting = false;

            MessagingCenter.Send(this as IShellUpdateMessage, "shell_update");
        }

    } // EleccionChangeViewModel
}
