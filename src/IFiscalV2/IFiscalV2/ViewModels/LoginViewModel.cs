using IFiscalV2.Models.Auth;
using IFiscalV2.Mvvm;
using IFiscalV2.Services.Auth;
using IFiscalV2.Services.Routing;
using IFiscalV2.Views;
using Splat;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IFiscalV2.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IRoutingService _routingService;
        private readonly IAuthService _authService;
        private readonly AppViewModel _appViewModel;
        bool isSigning = false;

        public LoginViewModel(IRoutingService routingService = null, IAuthService authService = null)
        {
            _instance = this;

            _routingService = routingService ?? Locator.Current.GetService<IRoutingService>();
            _authService = authService ?? Locator.Current.GetService<IAuthService>();

            _appViewModel = _appViewModel ?? Locator.Current.GetService<AppViewModel>();

            //ExecuteLogin = new Command(async () => await LoginAsync(), () => !isSigning);
            ExecuteLogin = new Command( async () =>  await LoginAsync(), () => !isSigning);
        }

        #region Singleton
        private static LoginViewModel _instance;
        public static LoginViewModel Instance => _instance is null ? new LoginViewModel() : _instance;
        #endregion

        public string Username { get; set; } = "pablo.admin";
        public string Password { get; set; } = "*1971@erPMT";
        public ICommand ExecuteLogin { get; set; }

        private async Task LoginAsync()
        {
            isSigning = true;

            var loginResult = await _authService.LoginAsync(new UserLogin { UserName = Username, Password = Password });

            if (loginResult.IsSuccess)
            {
                //TODO 1 -> (ARMAR ) : Funcion o Servicio ( Calculo de Ruta a navegar y Que opciones de Menu )
                isSigning = false;

                MessagingCenter.Send<LoginViewModel>(this, "shell_update");

                //await Shell.Current.Navigation.PopToRootAsync(animated: false);
                //await _routingService.NavigateToAsync("//main/page1");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Usuario no valido", "Por verifique el nombre de usuario y contraseña",  "Aceptar");
                isSigning = false;

                MessagingCenter.Send<LoginViewModel>(this, "username_focus");
                
            }
            
        }

        

    } // LoginViewModel

}
