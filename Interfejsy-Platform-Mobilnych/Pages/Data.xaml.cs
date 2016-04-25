using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Interfejsy_Platform_Mobilnych.ViewModel;

namespace Interfejsy_Platform_Mobilnych.Pages
{
    public sealed partial class Data
    {
        private DatabaseViewModel ViewModel { get; set; }
        private PositionViewModel PositionViewModel { get; }

        public Data()
        {
            InitializeComponent();
            PositionViewModel = new PositionViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = e.Parameter as DatabaseViewModel;
            if (ViewModel != null) PositionViewModel.InitPositions(ViewModel.SelectedDays);
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null) ViewModel.SetSelectedCurrency(grid.Tag as string);
            var page = MainGrid.Parent as Page;
            var frame = page?.Parent as Frame;
            frame?.Navigate(typeof(History), ViewModel);
        }

        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            PositionViewModel.InitPositions(ViewModel.GetCode(sender.Date));
        }
    }
}
