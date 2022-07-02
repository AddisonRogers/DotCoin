using System.Windows;
using System.Windows.Controls;

namespace DotCoinWPF
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var egg = (Window.GetWindow(App.Current.MainWindow) as MainWindow).MainFrame;
            egg.Content = new Page2();
        }
    }
}
