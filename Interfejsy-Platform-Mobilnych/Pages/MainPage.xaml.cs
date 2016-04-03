using Interfejsy_Platform_Mobilnych.Pages;
using Interfejsy_Platform_Mobilnych.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Interfejsy_Platform_Mobilnych
{
    public sealed partial class MainPage : Page
    {
        DatabaseViewModel ViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
            ViewModel = new DatabaseViewModel();
            ViewModel.InitPositions(ViewModel.SelectedDays);
            Loaded += TodayButton_Click;
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
