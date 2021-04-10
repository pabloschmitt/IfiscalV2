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
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            InitializeComponent();
        }

        internal LoadingViewModel ViewModel { get; set; } = Locator.Current.GetService<LoadingViewModel>();

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Init();
        }

    } // LoadingPage
}