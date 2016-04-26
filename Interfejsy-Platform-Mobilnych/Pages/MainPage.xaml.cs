using Windows.UI.Xaml;
using Interfejsy_Platform_Mobilnych.ViewModel;
using Windows.UI.Xaml.Navigation;

namespace Interfejsy_Platform_Mobilnych.Pages
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            ViewModel = new DatabaseViewModel();
            Loaded += TodayButton_Click;
        }

        private DatabaseViewModel ViewModel;
        
        private void TodayButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(Data), ViewModel);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}