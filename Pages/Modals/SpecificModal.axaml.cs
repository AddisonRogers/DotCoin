using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DotCoin3.Pages;

public partial class SpecificModal : Window
{
    public SpecificModal()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}