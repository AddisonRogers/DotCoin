using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DotCoin3;

public partial class error_page : UserControl
{
    public string error_code { get; set; }
    public error_page()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void StyledElement_OnInitialized(object? sender, EventArgs e)
    {
        this.Find<TextBlock>("error_code_block").Text = error_code;

    }
}