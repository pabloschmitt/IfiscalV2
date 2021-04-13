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
    public class SelectMesaViewModel : BaseViewModel, IShellUpdateMessage
    {
        private readonly IRoutingService _routingService;
        private readonly IAuthService _authService;
        private readonly IPageWorkflowServiceOptions _pageWorkflowServiceOptions;
        private readonly MesaService _mesaService;

        private IPageWorkflowService _pageWorkflowService => (_pageWorkflowServiceOptions as IPageWorkflowService);

        private ObservableCollection<MesaResponseModel> mesasCache;
        public ObservableCollection<MesaResponseModel> MesasCache
        {
            get { return mesasCache; }
            set { SetProperty(ref mesasCache, value); }
        }

        public ICommand TapCommand { get; private set; }

        public SelectMesaViewModel(
            IRoutingService routingService = null, IAuthService authService = null,
            IPageWorkflowServiceOptions pageWorkflowServiceOptions = null,
            MesaService mesaService = null
            )
        {
            _instance = this;

            _routingService = routingService ?? Locator.Current.GetService<IRoutingService>();
            _authService = authService ?? Locator.Current.GetService<IAuthService>();
            _pageWorkflowServiceOptions = _pageWorkflowServiceOptions ?? Locator.Current.GetService<IPageWorkflowServiceOptions>();
            _mesaService = mesaService ?? Locator.Current.GetService<MesaService>();

            //TapCommand = new Command<EleccionDto>((args) => OnTapped(args), (args) => !IsCommandExecuting);
        }

        #region Singleton
        private static SelectMesaViewModel _instance;
        public static SelectMesaViewModel Instance => _instance is null ? new SelectMesaViewModel() : _instance;
        #endregion

        public async Task LoadAsync()
        {
            IsBusy = true;

            var apiResult = await _mesaService.FindMesasAsync();

            if (apiResult.IsSuccess)
            {
                // Actualiza las mesas en la vista
                if (mesasCache == null)
                    mesasCache = new ObservableCollection<MesaResponseModel>();

                MesasCache = new ObservableCollection<MesaResponseModel>(apiResult.Result.Data);

                //Last_MesaEdited = null;
            }

            IsBusy = false;

        }

        public void OnTapped(MesaDto o)
        {
            IsCommandExecuting = true;

            //_pageWorkflowServiceOptions.SelectedEleccionId = o.Id;
            //_pageWorkflowServiceOptions.SelectedEleccionName = o.Name;
            //_pageWorkflowServiceOptions.InEleccion = true;
            //_pageWorkflowService.Save();

            IsCommandExecuting = false;

            //MessagingCenter.Send(this as IShellUpdateMessage, "shell_update");
        }

    } // SelectMesaViewModel

}
