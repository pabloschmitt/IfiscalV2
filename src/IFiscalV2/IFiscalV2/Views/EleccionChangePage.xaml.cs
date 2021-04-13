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
    public partial class EleccionChangePage : ContentPage
    {
        public EleccionChangePage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.LoadAsync();
        }

        internal EleccionChangeViewModel ViewModel { get; set; } = Locator.Current.GetService<EleccionChangeViewModel>();

    }
}