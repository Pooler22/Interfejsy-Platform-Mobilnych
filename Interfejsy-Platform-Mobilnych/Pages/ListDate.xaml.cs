using Interfejsy_Platform_Mobilnych.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Interfejsy_Platform_Mobilnych.Pages
{
    public sealed partial class ListDate : Page
    {
        DatabaseViewModel ViewModel { get; }
        
        public ListDate()
        {
            this.InitializeComponent();
            ViewModel = DatabaseViewModel.Instance;
            //ViewModel.Init();
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
            (mainFrame.Parent as Frame).Navigate(typeof(History), ViewModel.getTable((sender as TextBlock).Tag as string));
        }

    }
}
