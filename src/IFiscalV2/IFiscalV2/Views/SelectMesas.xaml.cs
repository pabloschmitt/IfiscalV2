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
    public partial class SelectMesas : ContentPage
    {
        public SelectMesas()
        {
            InitializeComponent();
            BindingContext = ViewModel;
        }

        private bool onAppearingCalled;
        protected async override void OnAppearing()
        {
            onAppearingCalled = false;

            base.OnAppearing();
            await ViewModel.LoadAsync();

            onAppearingCalled = true;

        }

        internal SelectMesaViewModel ViewModel { get; set; } = Locator.Current.GetService<SelectMesaViewModel>();

    } // SelectMesas
}