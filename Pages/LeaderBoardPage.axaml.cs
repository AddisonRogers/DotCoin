using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using Discord.Rest;
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

    private void Loaded(object? sender, EventArgs e) //TODO make the sort buttons usable
    {
        MW = (Window)this.Parent.Parent.Parent;
        //LeaderBoardChartSet();
        CryptoInfoTextBlockSet();
        CryptoSet();
        NewsSet();
        var dispatcherTimer = new DispatcherTimer();
        dispatcherTimer.Tick += dispatcherTimer_Tick;
        dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
        dispatcherTimer.Start();
    }
    private void dispatcherTimer_Tick(object? sender, EventArgs e) //TODO Add + optimize total market cap
    {
        //LeaderBoardChartSet();
        if (CurrentModal != null)
        {
            ModalOpen = true;
        }
    }

    /*private void LeaderBoardChartSet()
    {
        /*double[] dataX = new double[] { 1, 2, 3, 4, 5 };
        double[] dataY = new double[] { 1, 4, 9, 16, 25 };
        WpfPlot1.Plot.AddScatter(dataX, dataY);
        WpfPlot1.Refresh();#1# 
        
        // Fetch history works as : ID, Amount of units, Unit type
        // Ie Bitcoin, 2, D1 for bitcoin for the last 2 days
        
        
        int units;
        string unittype;
        units = 7;
        unittype = "d1";
        string[] unitlabel = new string[units];
        double[] unitdouble = new double[units]; 
        for (int i = 0; i < units; i++)
        {
            try
            {
                unitlabel[i] = (i+1).ToString();
                unitdouble[i] = i;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        //double[] prices = Fetch.History(name, units+1, unittype);
        Console.WriteLine("prices");
        if (prices.Length != unitdouble.Length)
        {
            Console.WriteLine("Error in charting");
            return;
        }
        AvaPlot chart = this.Find<AvaPlot>("Chart"); 
        if (ran)
        {
            chart.Plot.Clear();
        }
        chart.Plot.AddScatter(unitdouble, prices);
        switch (unittype)
        {
            case "d1":
                chart.Plot.XLabel("Days");
                break;
            case "h1":
                chart.Plot.XLabel("Hours");
                break;
            case "m1":
                chart.Plot.XLabel("Minutes");
                break;
        }
        chart.Plot.YLabel("Price");
        chart.Plot.Title("Price over time");
        chart.Plot.Grid(false);
        chart.Plot.XAxis.ManualTickPositions(unitdouble, unitlabel );
        chart.Refresh();

        //TODO Make the chart use a time system rather than relying off units so that I can upload a full minute for the past like year
    }*/

    private void CryptoInfoTextBlockSet() => this.Find<TextBlock>("CryptoInfoTextBlock").Text = System.IO.File.ReadAllText("C:\\Users\\Addison\\RiderProjects\\DotCoin\\Pages\\CryptoInfo.txt"); //TODO this
    
    //var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly(). CodeBase);
    //var iconPath = Path.Combine(outPutDirectory, "Folder\\Img.jpg");
    //string icon_path = new Uri(iconPath ).LocalPath;
    
    private void CryptoSet()
    {
        var AllCrypto = Fetch.GetAll();
        var CryptoList = this.Find<StackPanel>("CryptoListStackPanel");
        for (var i = 0; i < AllCrypto.AsArray().Count; i++)
        {
            var CryptoButton = new Button
            {
                Name = AllCrypto[i]?["id"].ToString(),
                Content = AllCrypto[i]?.ToString()
                
                //NewsTextBlock.Classes = 
                //TODO Styles
            };
            
            CryptoButton.Click += (sender, args) =>
            {
                var temp = new Specific { Id = CryptoButton.Name };
                this.Parent.Parent.Parent.Find<UserControl>("MVM").Content = temp;
            };
            CryptoList.Children.Add(CryptoButton);
        }
    }
    private void NewsSet() //TODO make it so when scroll down it adds another page
    {
        var News = Fetch.NewsGet(5); //TODO make it so it doesn't take that long
        var NewsList = this.Find<StackPanel>("NewsFeedStackPanel");
        for (var i = 0; i < News!.AsArray().Count; i++)
        {
            var NewsButton = new Button
            {
                Content = News[i]?["title"]?.ToString(),
                Name =  News[i]?["url"]?.ToString()
                //NewsTextBlock.Classes = 
            };
            NewsButton.Click += (sender, args) => Process.Start(new ProcessStartInfo("cmd", $"/c start {(sender as Button).Name.Replace("&", "^&")}") { CreateNoWindow = true });
            NewsList.Children.Add(NewsButton);
        }
    }
    private void CryptoInfoClicked(object? sender, PointerPressedEventArgs e) //TODO this 
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
}