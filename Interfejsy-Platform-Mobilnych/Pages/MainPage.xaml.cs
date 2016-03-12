using Interfejsy_Platform_Mobilnych.ViewModel;
using Windows.ApplicationModel.Background;
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
            InitializeComponent();
            ViewModel = DatabaseViewModel.Instance;
            ViewModel.Init();
        }

        DatabaseViewModel ViewModel { get; }

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

        public object BackgroundTaskSample { get; private set; }

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
            mainFrame.Navigate(typeof(Details));
        }

        private void DetailsButton1_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(History));
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

        private void TextBlock_PointerPressed(object sender, TappedRoutedEventArgs e)
        {
            var listBox = (((sender as TextBlock).Parent as StackPanel).Children[1] as ListBox);
            if (listBox.Visibility == Visibility.Collapsed)
            {
                listBox.Visibility = Visibility.Visible;
            }
            else
            {
                listBox.Visibility = Visibility.Collapsed;
            }
        }


        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(Details), ViewModel.getTable((sender as TextBlock).Tag as string));
            MySplitView.IsPaneOpen = false;
        }

        
    }
}
