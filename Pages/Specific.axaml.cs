using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using ScottPlot.Avalonia;
using System.Net;
using Avalonia.Interactivity;

namespace DotCoin3;

public partial class Specific : UserControl
{
    public Specific()
    {
        InitializeComponent();
    }

    public string Id { get; set; }
    private void SetText(double rate = 0)
    {
        var name = Id;
        
        var coin = Fetch.Get(name);
        this.Find<Button>("ToNative").Content = CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;
        this.Find<Button>("ToDollar").IsEnabled = true;
        this.Find<TextBlock>("SymbolBox").Text = "Symbol : " + coin?["symbol"]?.ToString();
        this.Find<TextBlock>("NameBox").Text = "Name : " + coin?["name"]?.ToString();
        this.Find<TextBlock>("SupplyBox").Text = "Circulating Supply : " + Math.Round(double.Parse(coin?["supply"]?.ToString() ?? string.Empty), 0);

        //This if is done becuause some crypto have unlimited supply
        if (coin?["maxSupply"]?.ToString() != null) this.Find<TextBlock>("MaxSupplyBox").Text = "Max Supply : " + Math.Round(double.Parse(coin?["maxSupply"]?.ToString() ?? string.Empty), 0);
        else this.Find<TextBlock>("MaxSupplyBox").Text = "Max Supply : " + "N/A";
        this.Find<TextBlock>("ChangeP24HBox").Text = "Change % 24H : " + Math.Round(double.Parse(coin?["changePercent24Hr"]?.ToString() ?? string.Empty), 2);


        if (rate == 0)
        {
            this.Find<TextBlock>("MarketCapBox").Text = "Market Cap : " + Math.Round(double.Parse(coin?["marketCapUsd"]?.ToString() ?? string.Empty), 0);
            this.Find<TextBlock>("PriceBox").Text = "Price : " + Math.Round(double.Parse(coin?["priceUsd"]?.ToString() ?? string.Empty), 2);
            this.Find<TextBlock>("Change24HBox").Text = "Change 24H : " + Math.Round(double.Parse(coin?["volumeUsd24Hr"]?.ToString() ?? string.Empty), 2); 

        }
        else
        {
            this.Find<TextBlock>("MarketCapBox").Text = "Market Cap : " + Math.Round(double.Parse(coin?["marketCapUsd"]?.ToString() ?? string.Empty) * rate, 0);
            this.Find<TextBlock>("PriceBox").Text = "Price : " + Math.Round(double.Parse(coin?["priceUsd"]?.ToString() ?? string.Empty) * rate, 2);
            this.Find<TextBlock>("Change24HBox").Text = "Change 24H : " + Math.Round(double.Parse(coin?["volumeUsd24Hr"]?.ToString() ?? string.Empty) * rate, 2);
        }
        //MovingAverageBox.Text = "Moving Average : " + Math.Round(double.Parse(Indicator.MovingAverage(name)?.ToString() ?? string.Empty), 0);
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    private async void Chartstart(bool ran = false)
    {
        /*double[] dataX = new double[] { 1, 2, 3, 4, 5 };
        double[] dataY = new double[] { 1, 4, 9, 16, 25 };
        WpfPlot1.Plot.AddScatter(dataX, dataY);
        WpfPlot1.Refresh();*/ 
        
        // Fetch history works as : ID, Amount of units, Unit type
        // Ie Bitcoin, 2, D1 for bitcoin for the last 2 days
        
        var name = Id;
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
        double[] prices = Fetch.History(name, units+1, unittype);
        Console.WriteLine(prices);
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
    }
    private void StyledElement_OnInitialized(object? sender, EventArgs e)
    {
        SetText();
        Chartstart();
        
        
        
        
        
        var dispatcherTimer = new DispatcherTimer();
        dispatcherTimer.Tick += dispatcherTimer_Tick;
        dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
        dispatcherTimer.Start();
    }

    private void dispatcherTimer_Tick(object? sender, EventArgs e)
    {
        SetText();
        Chartstart(true);
    }

    private void ToDollar_OnClick(object? sender, RoutedEventArgs e)
    {
        SetText();
    }

    private void ToNative_OnClick(object? sender, RoutedEventArgs e)
    {
        SetText(double.Parse(Fetch.GetRates(Fetch.GetFiat())[0,1] ?? string.Empty));
    }
    
}