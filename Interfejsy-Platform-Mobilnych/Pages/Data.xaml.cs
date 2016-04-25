using Interfejsy_Platform_Mobilnych.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Interfejsy_Platform_Mobilnych
{
    public sealed partial class Data : Page
    {
        private DatabaseViewModel ViewModel { get; set; }
        private PositionViewModel PositionViewModel { get; set; }

        public Data()
        {
            InitializeComponent();
            PositionViewModel = new PositionViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = e.Parameter as DatabaseViewModel;
            PositionViewModel.InitPositions(ViewModel.SelectedDays);
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.SetSelectedCurrency((sender as Grid).Tag as string);
            ((MainGrid.Parent as Page).Parent as Frame).Navigate(typeof(History), ViewModel);
        }

        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            PositionViewModel.InitPositions(ViewModel.GetCode((sender as CalendarDatePicker).Date));
        }
    }
}
