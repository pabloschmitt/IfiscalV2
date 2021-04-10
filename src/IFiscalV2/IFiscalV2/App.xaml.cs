using IFiscalV2.Services.Auth;
using IFiscalV2.Services.Routing;
using IFiscalV2.ViewModels;
using IFiscalV2.Views;
using Splat;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IFiscalV2
{
    public partial class App : Application
    {

        public App()
        {
            InitializeDi();
            InitializeComponent();

            MainPage = new AppShell();

        }

        private void InitializeDi()
        {
            // Services
            Locator.CurrentMutable.RegisterLazySingleton<IRoutingService>(() => new ShellRoutingService());
            Locator.CurrentMutable.RegisterLazySingleton<IAuthService>(() => new AuthService());

            // ViewModels
            Locator.CurrentMutable.Register(() => new LoadingViewModel());
            Locator.CurrentMutable.Register(() => LoginViewModel.Instance);

            // Siempre al Final
            Locator.CurrentMutable.Register(() => AppViewModel.Instance);
        }

        //protected async override void OnStart()
        //{
        //    //// Se Fija el el login anterior
        //    //var svc = AuthService.GetInstance();
        //    ////var loginResult = await svc.CheckPreviusLoginAsync();

        //    //var loginResult = new Models.Auth.LoginResult() { IsSuccess = false };

        //    //await Shell.Current.GoToAsync("//LoginPage");

        //    //if (!loginResult.IsSuccess)
        //    //{
        //    //    //await Shell.Current.GoToAsync("//LoginPage");
        //    //}
        //    //else
        //    //{
        //    //    //MainVM.SetMainPageFrom(loginResult);
        //    //}

        //}

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
