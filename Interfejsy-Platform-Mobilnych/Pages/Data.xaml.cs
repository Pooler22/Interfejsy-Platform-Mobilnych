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
            // Render basic day items.
            if (args.Phase == 0)
            {
                // Register callback for next phase.
                args.RegisterUpdateCallback(CalendarDatePicker_OnCalendarViewDayItemChanging);
            }
            // Set blackout dates.
            else if (args.Phase == 1)
            {
                // Blackout dates in the past, Sundays, and dates that are fully booked.
                if (ViewModel.HasDate(args.Item.Date) == false
                    )
                {
                    args.Item.IsBlackout = true;
                }
                // Register callback for next phase.
                args.RegisterUpdateCallback(CalendarDatePicker_OnCalendarViewDayItemChanging);
            }
            // Set density bars.
            else if (args.Phase == 2)
            {
                // Avoid unnecessary processing.
                // You don't need to set bars on past dates or Sundays.
                if (args.Item.Date > DateTimeOffset.Now &&
                    args.Item.Date.DayOfWeek != DayOfWeek.Sunday)
                {
                    // Get bookings for the date being rendered.
                    //var currentBookings = Bookings.GetBookings(args.Item.Date);

                    List<Color> densityColors = new List<Color>();
                    // Set a density bar color for each of the days bookings.
                    // It's assumed that there can't be more than 10 bookings in a day. Otherwise,
                    // further processing is needed to fit within the max of 10 density bars.
                    //foreach (booking in currentBookings)
                    //{
                    //    if (booking.IsConfirmed == true)
                    //    {
                    //        densityColors.Add(Colors.Green);
                    //    }
                    //    else
                    //    {
                    //        densityColors.Add(Colors.Blue);
                    //    }
                    //}
                    args.Item.SetDensityColors(densityColors);
                }
            }
        }
    }
}