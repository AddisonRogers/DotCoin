using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DotCoinTwo.Views;

public partial class News : UserControl
{
    public News()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}