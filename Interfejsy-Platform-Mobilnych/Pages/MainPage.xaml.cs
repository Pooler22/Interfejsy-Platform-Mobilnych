using Windows.UI.Xaml;
using Interfejsy_Platform_Mobilnych.ViewModel;

namespace Interfejsy_Platform_Mobilnych.Pages
{
    public sealed partial class MainPage
    {
        private readonly DatabaseViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new DatabaseViewModel();
            Loaded += TodayButton_Click;
        }

        private void TodayButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(Data), _viewModel);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}