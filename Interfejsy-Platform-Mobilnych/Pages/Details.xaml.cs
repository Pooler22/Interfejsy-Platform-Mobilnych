using Interfejsy_Platform_Mobilnych.Models;
using Interfejsy_Platform_Mobilnych.Modules;
using Interfejsy_Platform_Mobilnych.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace Interfejsy_Platform_Mobilnych
{
    public sealed partial class Details : Page
    {
        TableViewModel ViewModel { get; }

        public Details()
        {
            this.InitializeComponent();
            //var ala1 = Downloader.Get("http://www.nbp.pl/kursy/xml/dir.txt");
            //ala.Text = ala1;

            ViewModel = new TableViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.init(e.Parameter as Table);
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            (mainGrid.Parent as Frame).Navigate(typeof(History), (sender as TextBlock).Tag);
        }
    }
}
