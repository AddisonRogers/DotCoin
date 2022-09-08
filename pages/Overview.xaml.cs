using System.Windows;
using System.Windows.Controls;

namespace DotCoinWPF.pages;

/// <summary>
///     Interaction logic for Page1.xaml
/// </summary>
public partial class Page1 : Page
{
    public Page1()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (Application.Current.MainWindow != null)
        {
            var egg = (Window.GetWindow(Application.Current.MainWindow) as MainWindow)?.MainFrame;
            egg.Content = new Page2("bitcoin");
        }
    }
}