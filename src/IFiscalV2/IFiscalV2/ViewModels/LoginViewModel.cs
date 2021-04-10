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
            ExecuteLogin = new Command( () =>  Login(), () => !isSigning);
        }

        #region Singleton
        private static LoginViewModel _instance;
        public static LoginViewModel Instance => _instance is null ? new LoginViewModel() : _instance;
        #endregion

        public string Username { get; set; } = "pablo.admin";
        public string Password { get; set; } = "123456";
        public ICommand ExecuteLogin { get; set; }

        private void Login()
        {
            isSigning = true;

            _appViewModel.IsStarting = false;

            var loginResult = _authService.LoginAsync(new UserLogin { UserName = Username, Password = Password }).Result;

            if (loginResult.IsSuccess)
            {
               //TODO

                Shell.Current.Navigation.PopToRootAsync(animated: false);
                _routingService.NavigateToAsync("//main/page1");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert(
                    "Usuario no valido",
                    "Por verifique el nombre de usuario y contraseña",
                    "Aceptar");
            }

            isSigning = false;
        }

        private async Task LoginAsync()
        {
            

            await Task.Delay(10);



            //var isAuthenticated = _authService.LoginAsync(new UserLogin { UserName = Username, Password = Password }).Result;

            //await _routingService.NavigateToAsync("//main/home");

            //var isAuthenticated = await _authService.LoginAsync( new UserLogin { UserName = Username, Password = Password });
            //if (isAuthenticated.IsSuccess)
            //{
            //    //await Shell.Current.Navigation.PopToRootAsync();
            //    //await Shell.Current.Navigation.PopAsync();
            //    //await Shell.Current.GoToAsync("//main/home");
            //    //await Task.Delay(150);
            //    await _routingService.NavigateToAsync("//main/home");
            //}
            //else
            //{
            //    await Application.Current.MainPage.DisplayAlert(
            //        "Usuario no valido",
            //        "Por verifique el nombre de usuario y contraseña",
            //        "Aceptar");
            //}
            isSigning = false;
        }

    } // LoginViewModel

}
