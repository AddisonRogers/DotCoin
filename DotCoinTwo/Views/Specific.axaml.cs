using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DotCoinTwo.Views;

public partial class Specific : UserControl
{
    public Specific(string id)
    {
        InitializeComponent();
        ViewModels.SpecificViewModel vm = new ViewModels.SpecificViewModel(id);
        DataContext = vm;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
}