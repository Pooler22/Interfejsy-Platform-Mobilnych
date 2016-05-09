using Windows.ApplicationModel.Activation;
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

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                //TODO: Load state from previously suspended application
            }
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