﻿using Interfejsy_Platform_Mobilnych.ViewModel;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Interfejsy_Platform_Mobilnych
{
    public sealed partial class History : Page
    {
        public History()
        {
            InitializeComponent();
        }

        DatabaseViewModel ViewModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = e.Parameter as DatabaseViewModel;
            StartDate.MinYear = ViewModel.MinAvailableDate;
            StartDate.MaxYear = DateTime.Today;
            ;
//            ViewModel.MaxAvailableDate;
        }
    }
}
