using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DotCoinWPF.pages;

/// <summary>
///     Interaction logic for Page2.xaml
/// </summary>
public partial class Page2 : Page
{
    public Page2(string name)
    {
        InitializeComponent();
        refreshcoin(name);
    }

    private async void refreshcoin(string name)
    {
        Title = name;
        var coin = Fetch.Get(name);
        SymbolBox.Text = "Symbol : "+coin?["symbol"]?.ToString();
        NameBox.Text = "Name : " + coin?["name"];
        SupplyBox.Text = "Circulating Supply : " +
                         Math.Round(double.Parse(coin?["supply"]?.ToString() ?? string.Empty), 0);
        if (coin?["maxSupply"]?.ToString() != null)
            MaxSupplyBox.Text = "Max Supply : " +
                                Math.Round(double.Parse(coin?["maxSupply"]?.ToString() ?? string.Empty), 0);
        else
            MaxSupplyBox.Text = "Max Supply : " + "N/A";
        MarketCapBox.Text = "Market Cap : " +
                            Math.Round(double.Parse(coin?["marketCapUsd"]?.ToString() ?? string.Empty), 0);
        PriceBox.Text = "Price : $" + Math.Round(double.Parse(coin?["priceUsd"]?.ToString() ?? string.Empty), 2);
        Change24HBox.Text = "Volume change in 24h : " +
                            Math.Round(double.Parse(coin?["volumeUsd24Hr"]?.ToString() ?? string.Empty), 0);
        ChangeP24HBox.Text = "% Change in 24h : " +
                             Math.Round(double.Parse(coin?["changePercent24Hr"]?.ToString() ?? string.Empty), 1) +
                             "%"; //TODO should probably redo this and make it more complex etc. (Also add a graph)

        MovingAverageBox.Text = "Moving Average : " +
                                Math.Round(double.Parse(Indicator.MovingAverage(name)?.ToString() ?? string.Empty), 0);
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        var dispatcherTimer = new DispatcherTimer();
        dispatcherTimer.Tick += dispatcherTimer_Tick;
        dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
        dispatcherTimer.Start();
    }

    private void dispatcherTimer_Tick(object sender, EventArgs e)
    {
        refreshcoin(Title);
    }
}