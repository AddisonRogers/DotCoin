using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace DotCoin3.Pages;

public partial class LeaderBoardPage : UserControl
{
    public LeaderBoardPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void Loaded(object? sender, EventArgs e)
    {
        LeaderBoardChartSet();
        CryptoInfoTextBlockSet();
    }

    private void LeaderBoardChartSet()
    {
        
    }

    private void CryptoInfoTextBlockSet()
    {
        
    }

    private void CryptoInfoClicked(object? sender, PointerPressedEventArgs e)
    {
        
    }
}