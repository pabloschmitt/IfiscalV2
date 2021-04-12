using IFiscalV2.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IFiscalV2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SiteChangePage : ContentPage
    {
        public SiteChangePage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.LoadSitesAsync();
        }
        internal SiteChangeViewModel ViewModel { get; set; } = Locator.Current.GetService<SiteChangeViewModel>();

    }
}