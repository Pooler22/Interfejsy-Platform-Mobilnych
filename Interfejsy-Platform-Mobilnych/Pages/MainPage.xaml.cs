using Interfejsy_Platform_Mobilnych.Pages;
using Interfejsy_Platform_Mobilnych.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Interfejsy_Platform_Mobilnych
{
    public sealed partial class MainPage : Page
    {
        DatabaseViewModel ViewModel { get; }

        public MainPage()
        {
            InitializeComponent();
            ViewModel = DatabaseViewModel.Instance;
            ViewModel.Init();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainFrame.Navigate(typeof(Today));
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void TodayButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(Today), ViewModel.getLastRates());
        }

        private void ArchiveButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(Archive));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
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
            mainFrame.Navigate(typeof(Today), ViewModel.getTable((sender as TextBlock).Tag as string));
            MySplitView.IsPaneOpen = false;
        }
    }
}
