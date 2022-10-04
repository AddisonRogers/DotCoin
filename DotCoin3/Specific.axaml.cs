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

    public string id;
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
        this.Find<TextBlock>("MarketCapBox").Text = "Market Cap : " + Math.Round(double.Parse(coin?["marketCapUsd"]?.ToString() ?? string.Empty), 0);
        //this.Find<TextBlock>("PriceBox").Text = "Price : " + Math.Round(double.Parse(coin?["price"]?.ToString() ?? string.Empty), 2);
        //this.Find<TextBlock>("Change24HBox").Text = "Change 24H : " + Math.Round(double.Parse(coin?["change24h"]?.ToString() ?? string.Empty), 2); 
        //this.Find<TextBlock>("ChangeP24HBox").Text = "Change % 24H : " + Math.Round(double.Parse(coin?["changeP24h"]?.ToString() ?? string.Empty), 2);
        //MovingAverageBox.Text = "Moving Average : " + Math.Round(double.Parse(Indicator.MovingAverage(name)?.ToString() ?? string.Empty), 0);
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}