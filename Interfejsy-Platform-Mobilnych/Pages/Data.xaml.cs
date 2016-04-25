using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Interfejsy_Platform_Mobilnych.ViewModel;

namespace Interfejsy_Platform_Mobilnych.Pages
{
    public sealed partial class Data
    {
        public Data()
        {
            InitializeComponent();
            PositionViewModel = new PositionViewModel();
        }

        private DatabaseViewModel ViewModel { get; set; }
        private PositionViewModel PositionViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = e.Parameter as DatabaseViewModel;
            if (ViewModel != null) PositionViewModel.InitPositions(ViewModel.SelectedDays);
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.SetSelectedCurrency((sender as Grid)?.Tag as string);
            ((MainGrid.Parent as Page)?.Parent as Frame)?.Navigate(typeof(History), ViewModel);
        }

        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender,
            CalendarDatePickerDateChangedEventArgs args)
        {
            PositionViewModel.InitPositions(ViewModel.GetCode(sender.Date));
        }
    }
}