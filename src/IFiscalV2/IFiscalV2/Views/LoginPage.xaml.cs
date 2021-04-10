using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IFiscalV2.ViewModels;
using Splat;

namespace IFiscalV2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {

            InitializeComponent();
            BindingContext = ViewModel;
            //Shell.Current.FlyoutIsPresented = false;
        }

        internal LoginViewModel ViewModel { get; set; } = Locator.Current.GetService<LoginViewModel>();
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

    } // LoginPage
}