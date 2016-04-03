using Interfejsy_Platform_Mobilnych.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Interfejsy_Platform_Mobilnych.Pages
{
    public sealed partial class Date : Page
    {
        DatabaseViewModel ViewModel { get; set; }

        public Date()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = e.Parameter as DatabaseViewModel;
            //var items = kalendarz.item;
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
            ViewModel.SelectedDays = (sender as TextBlock).Tag as string;
            (mainFrame.Parent as Frame).Navigate(typeof(Data), ViewModel);
        }
    }
}
