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
            init();
        }

        async void init()
        {
            await ViewModel.Init();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.Init();
            mainFrame.Navigate(typeof(Today));
        }

        private void TodayButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(Today));
        }

        private void ArchiveButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(ListDate));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
