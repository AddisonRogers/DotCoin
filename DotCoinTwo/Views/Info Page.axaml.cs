using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DotCoinTwo.Views;

public partial class Info_Page : UserControl
{
    public Info_Page()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}