using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Interfejsy_Platform_Mobilnych
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainFrame.Navigate(typeof(Details));
            MySplitView.IsPaneOpen = false;

            BackButtonVisibility = Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            SystemNavigationManager.GetForCurrentView().BackRequested += BackButtonPage_BackRequested;
            base.OnNavigatedTo(e);
        }

        public AppViewBackButtonVisibility BackButtonVisibility
        {
            get { return SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility; }
            set { SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = value; }
        }

        private void BackButtonPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            OnBackRequested(sender, e);
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Details));
        }

        private void ExchangeRateListItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(Details));
            MySplitView.IsPaneOpen = false;
        }

        private void ExitListItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Application.Current.Exit();
        }

    }
}
