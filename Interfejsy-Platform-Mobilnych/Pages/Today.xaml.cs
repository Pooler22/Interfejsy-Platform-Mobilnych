using Interfejsy_Platform_Mobilnych.Models;
using Interfejsy_Platform_Mobilnych.ViewModel;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Interfejsy_Platform_Mobilnych
{
    public sealed partial class Today : Page
    {
        DayViewModel ViewModel { get; }

        public Today()
        {
            InitializeComponent();
            ViewModel = new DayViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Init(e.Parameter as List<string>);
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ((mainGrid.Parent as Page).Parent as Frame).Navigate(typeof(History), (sender as Grid).Tag);
        }
    }
}
