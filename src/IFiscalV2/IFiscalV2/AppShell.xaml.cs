using IFiscalV2.Services.Auth;
using IFiscalV2.ViewModels;
using IFiscalV2.Views;
using Xamarin.Forms;

namespace IFiscalV2
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        //public AppViewModel ShellViewModel { get; set; }

        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("//main/login", typeof(LoginPage));
            //Routing.RegisterRoute("//main/home", typeof(MainPage));
            //Routing.RegisterRoute("//main/page1", typeof(Page1));
            //Routing.RegisterRoute("//main/page2", typeof(Page2));
            //Routing.RegisterRoute("//main/page3", typeof(Page3));

            //Routing.RegisterRoute("//main/menu/page1", typeof(Page1));

            BindingContext = AppViewModel.Instance;

            if ((BindingContext as AppViewModel).IsStarting)
                (BindingContext as AppViewModel).IsStarting = false;
        } // AppShell CTOR





    } // AppShell

}
