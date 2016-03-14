using Interfejsy_Platform_Mobilnych.Models;
using Interfejsy_Platform_Mobilnych.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Interfejsy_Platform_Mobilnych
{
    public sealed partial class Today : Page
    {
        TableViewModel ViewModel { get; }

        public Today()
        {
            InitializeComponent();
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
