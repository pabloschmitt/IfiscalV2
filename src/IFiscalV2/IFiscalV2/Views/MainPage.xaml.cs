﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IFiscalV2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            //Shell.Current.IsEnabled = true;
            ////Shell.Current.FlyoutIsPresented = false;
        }
    }
}