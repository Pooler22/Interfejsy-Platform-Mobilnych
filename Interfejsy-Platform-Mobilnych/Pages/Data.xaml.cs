using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Interfejsy_Platform_Mobilnych.Models;
using Interfejsy_Platform_Mobilnych.ViewModel;
using Syncfusion.UI.Xaml.Grid;

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
            PositionViewModel.InitPositions(ViewModel?.SelectedDays);
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

        private void CalendarDatePicker_OnCalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            if (args.Phase == 0)
            {
                args.RegisterUpdateCallback(CalendarDatePicker_OnCalendarViewDayItemChanging);
            }
            else if (args.Phase == 1)
            {
                if (ViewModel.HasDate(args.Item.Date) == false)
                {
                    args.Item.IsBlackout = true;
                }
                args.RegisterUpdateCallback(CalendarDatePicker_OnCalendarViewDayItemChanging);
            }
            else if (args.Phase == 2)
            {
                if (args.Item.Date > DateTimeOffset.Now &&
                    args.Item.Date.DayOfWeek != DayOfWeek.Sunday)
                {
                    List<Color> densityColors = new List<Color>();
                    args.Item.SetDensityColors(densityColors);
                }
            }
        }
    }
}