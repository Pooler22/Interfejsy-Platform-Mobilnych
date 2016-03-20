using Interfejsy_Platform_Mobilnych.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Interfejsy_Platform_Mobilnych
{
    public sealed partial class Data : Page
    {
        DatabaseViewModel ViewModel { get; set; }
        DayViewModel DayViewModel { get; set; }

        public Data()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = e.Parameter as DatabaseViewModel;
            DayViewModel = new DayViewModel();
            DayViewModel.Init(ViewModel.SelectedDays);
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.SelectedCurrency = (sender as Grid).Tag as string;
            ((mainGrid.Parent as Page).Parent as Frame).Navigate(typeof(History), ViewModel);
        }
    }
}
