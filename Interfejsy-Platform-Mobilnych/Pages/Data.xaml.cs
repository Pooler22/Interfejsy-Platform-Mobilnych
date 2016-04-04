using Interfejsy_Platform_Mobilnych.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Interfejsy_Platform_Mobilnych
{
    public sealed partial class Data : Page
    {
        DatabaseViewModel ViewModel { get; set; }

        public Data()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = e.Parameter as DatabaseViewModel;
            ViewModel.InitPositions(ViewModel.SelectedDays);
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.setSelectedCurrency((sender as Grid).Tag as string);
            ((mainGrid.Parent as Page).Parent as Frame).Navigate(typeof(History), ViewModel);
        }

        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            ViewModel.InitPositions(ViewModel.GetCode((sender as CalendarDatePicker).Date));
        }
    }
}
