using Interfejsy_Platform_Mobilnych.Pages;
using Interfejsy_Platform_Mobilnych.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Interfejsy_Platform_Mobilnych
{
    public sealed partial class MainPage : Page
    {
        DatabaseViewModel ViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
            ViewModel = DatabaseViewModel.Instance;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Init();
            mainFrame.Navigate(typeof(Data), ViewModel);
        }

        private void TodayButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(Data), ViewModel);
        }

        private void ArchiveButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(Date), ViewModel);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
