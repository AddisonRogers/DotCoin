using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace DotCoin3.Pages;

public partial class CryptoInfoModal : Window
{
    public CryptoInfoModal()
    {
           
        InitializeComponent();
        //this.FrameSize = new Size(this.Parent.Parent.Width/2, MainWindow.HeightProperty/2); 
#if DEBUG
        this.AttachDevTools();
#endif
    }

    public string title;
    public string info;
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        this.Find<TextBlock>("Title").Text = title;
        this.Find<TextBlock>("Info").Text = info;
    }

    private void InputElement_OnLostFocus(object? sender, RoutedEventArgs e)
    {
        //TODO Stop showing the modal
        this.Close();
        
    }

    private void TopLevel_OnClosed(object? sender, EventArgs e)
    {
        //throw new NotImplementedException();
    }
}