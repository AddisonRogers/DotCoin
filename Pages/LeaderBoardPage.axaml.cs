using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using ScottPlot.Avalonia;

namespace DotCoin3.Pages;

public partial class LeaderBoardPage : UserControl
{
    Window MW;
    bool ModalOpen = false;
    Window CurrentModal = null;
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
        MW = (Window)this.Parent.Parent.Parent;
        //LeaderBoardChartSet();
        CryptoInfoTextBlockSet();
        CryptoSetNon();
        NewsSet();
        var dispatcherTimer = new DispatcherTimer();
        dispatcherTimer.Tick += dispatcherTimer_Tick;
        dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
        dispatcherTimer.Start();
    }
    private void dispatcherTimer_Tick(object? sender, EventArgs e) 
    {
        
        if (CurrentModal != null)
        {
            ModalOpen = true;
        }
    }

    public bool ran = false;
    private void LeaderBoardChartSet() //TODO only run this once somehow on the program
    {
        int units = 7;
        string[] unitlabel = new string[units];
        double[] unitdouble = new double[units]; 
        for (int i = 0; i < units; i++)
        {
            unitlabel[i] = (i+1).ToString();
            unitdouble[i] = i;
        }
        double[] prices = Fetch.GetCryptoMarketCap(units);
        if (prices.Length != unitdouble.Length)
        {
            Console.WriteLine("Error in charting");
            return;
        }
        AvaPlot chart = this.Find<AvaPlot>("Chart"); 
        if (ran) chart.Plot.Clear();
        chart.Plot.AddScatter(unitdouble, prices);
        chart.Plot.XLabel("Days");
        chart.Plot.YLabel("Price");
        chart.Plot.Title("Price over time");
        chart.Plot.Grid(false);
        chart.Plot.XAxis.ManualTickPositions(unitdouble, unitlabel);
        chart.Refresh();
        ran = true;
    }

    private void CryptoInfoTextBlockSet() => this.Find<TextBlock>("CryptoInfoTextBlock").Text = System.IO.File.ReadAllText("C:\\Users\\Addison\\RiderProjects\\DotCoin\\Pages\\CryptoInfo.txt"); //TODO this
    
    //var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly(). CodeBase);
    //var iconPath = Path.Combine(outPutDirectory, "Folder\\Img.jpg");
    //string icon_path = new Uri(iconPath ).LocalPath;
    
    private void CryptoSetNon(Dictionary<string,string>[] AllCrypto = null)
    {
        if (AllCrypto == null) AllCrypto = Sort.Convert(Fetch.GetAll());
        var CryptoList = this.Find<StackPanel>("CryptoListStackPanel");
        if (CryptoList.Children.Count > 5) CryptoList.Children.Clear();
        for (var i = 0; i < AllCrypto.Length; i++)
        {
            var CryptoButton = new Button
            {
                Name = AllCrypto[i]?["id"].ToString(),
                Content = (AllCrypto[i]?["id"] ," "+ AllCrypto[i]?["priceUsd"])   
                
                //NewsTextBlock.Classes = 
                
            };
            
            CryptoButton.Click += (sender, args) =>
            {
                var temp = new Specific { Id = CryptoButton.Name };
                this.Parent.Parent.Parent.Find<UserControl>("MVM").Content = temp;
            };
            CryptoList.Children.Add(CryptoButton);
        }
    }

    private void NewsSet(int NewsCount = 0)
    {
        var News = Fetch.NewsGet(5, NewsCount); 
        var NewsList = this.Find<StackPanel>("NewsFeedStackPanel");
        for (var i = 0; i < News!.AsArray().Count; i++)
        {
            var NewsButton = new Button
            {
                Content = News[i]?["title"]?.ToString(),
                Name = News[i]?["url"]?.ToString()
                //NewsTextBlock.Classes = 
            };
            NewsButton.Click += (sender, args) =>
                Process.Start(new ProcessStartInfo("cmd", $"/c start {(sender as Button).Name.Replace("&", "^&")}")
                    { CreateNoWindow = true });
            NewsList.Children.Add(NewsButton);
        }

        var temp = new Button() { Content = "More News", Name = "" };
        temp.Click += (sender, args) => NewsSet(NewsCount += 5);
        NewsList.Children.Add(temp);
    }

    private int NewsCount = 0;

    private void CryptoInfoClicked(object? sender, PointerPressedEventArgs e)
    {
        var smth = new CryptoInfoModal()
        {
            Height = (MW.Height)/2,
            Width = (MW.Width)/2,
            title = "Crypto Info",
            info = System.IO.File.ReadAllText("C:\\Users\\Addison\\RiderProjects\\DotCoin\\Pages\\CryptoInfo.txt")
        };
        smth.Show((Window)MW);
        CurrentModal = smth;
    }
    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (ModalOpen)
        {
            CurrentModal.Close();
            ModalOpen = false;
            CurrentModal = null;
        }
    }


    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        switch (((Button)sender).Content)
        {
            case "Price":
                CryptoSetNon(Sort.SelectionSort(Fetch.GetAll(), "priceUsd"));
                break;
        }
    }
    
}