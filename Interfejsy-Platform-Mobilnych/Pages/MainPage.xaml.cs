using Windows.UI.Xaml;
using Interfejsy_Platform_Mobilnych.ViewModel;

namespace Interfejsy_Platform_Mobilnych.Pages
{
    public sealed partial class MainPage
    {
        private DatabaseViewModel ViewModel { get; }

        public MainPage()
        {
            InitializeComponent();
            ViewModel = new DatabaseViewModel();
            //ViewModel.InitPositions(ViewModel.SelectedDays);
            Loaded += TodayButton_Click;
        }

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
