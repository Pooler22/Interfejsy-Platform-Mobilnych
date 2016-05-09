using Windows.ApplicationModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Interfejsy_Platform_Mobilnych.Models;
using Interfejsy_Platform_Mobilnych.ViewModel;
using Syncfusion.UI.Xaml.Grid;

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
            PositionViewModel.InitPositions(ViewModel?.SelectedDays);
            ViewModel.SetLatsPage("Data");
        }

        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender,
            CalendarDatePickerDateChangedEventArgs args)
        {
            PositionViewModel.InitPositions(ViewModel.GetCode(sender.Date));
        }

        private void SfDataGrid_OnSelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            ViewModel.SelectedCurrency = ((sender as SfDataGrid)?.SelectedItem as Position)?.Name;
            ((MainGrid.Parent as Page)?.Parent as Frame)?.Navigate(typeof(History), ViewModel);
        }

        private void CalendarDatePicker_OnCalendarViewDayItemChanging(CalendarView sender,
            CalendarViewDayItemChangingEventArgs args)
        {
            ViewModel.CheckBlackout(args);
        }

        protected override async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            await ViewModel.SaveStateAsync();
        }
    }
}