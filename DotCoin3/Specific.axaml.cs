using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DotCoin3;

public partial class Specific : UserControl
{
    public Specific()
    {
        InitializeComponent();
        SetText();
    }

    public string id = "bitcoin";
    private void SetText()
    {
        var name = id;
        var coin = Fetch.Get(name);
        this.Find<TextBlock>("SymbolBox").Text = "Symbol : " + coin?["symbol"]?.ToString();
        this.Find<TextBlock>("NameBox").Text = "Name : " + coin?["name"]?.ToString();
        this.Find<TextBlock>("SupplyBox").Text = "Circulating Supply : " + Math.Round(double.Parse(coin?["supply"]?.ToString() ?? string.Empty), 0);
        
        //This if is done becuause some crypto have unlimited supply
        if (coin?["maxSupply"]?.ToString() != null) this.Find<TextBlock>("MaxSupplyBox").Text = "Max Supply : " + Math.Round(double.Parse(coin?["maxSupply"]?.ToString() ?? string.Empty), 0);
        else this.Find<TextBlock>("MaxSupplyBox").Text = "Max Supply : " + "N/A";
        MarketCapBox.Text = "Market Cap : " + Math.Round(double.Parse(coin?["marketCapUsd"]?.ToString() ?? string.Empty), 0);
        PriceBox.Text = "Price : $" + Math.Round(double.Parse(coin?["priceUsd"]?.ToString() ?? string.Empty), 2);
        Change24HBox.Text = "Volume change in 24h : " + Math.Round(double.Parse(coin?["volumeUsd24Hr"]?.ToString() ?? string.Empty), 0);
        ChangeP24HBox.Text = "% Change in 24h : " + Math.Round(double.Parse(coin?["changePercent24Hr"]?.ToString() ?? string.Empty), 1) + "%";
        //MovingAverageBox.Text = "Moving Average : " + Math.Round(double.Parse(Indicator.MovingAverage(name)?.ToString() ?? string.Empty), 0);
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}