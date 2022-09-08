﻿using System;
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
        chartstart(name);
    }

    private async void refreshcoin(string name)
    {
        Title = name;
        var coin = Fetch.Get(name);
        SymbolBox.Text = "Symbol : " + coin?["symbol"]?.ToString();
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
                                                                                      "%"; 

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
    private async void chartstart(string name)
    {
        /*double[] dataX = new double[] { 1, 2, 3, 4, 5 };
        double[] dataY = new double[] { 1, 4, 9, 16, 25 };
        WpfPlot1.Plot.AddScatter(dataX, dataY);
        WpfPlot1.Refresh();*/ 
        
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

        double[] prices = Fetch.History(name, units+1, unittype);
        Console.WriteLine(prices);
        if (prices.Length != unitdouble.Length)
        {
            Console.WriteLine("Error in charting");
            return;
        }
        
        
        Chart.Plot.AddScatter(unitdouble, prices);
        if (unittype == "d1")
        {
            Chart.Plot.XLabel("Days");
        }
        else if (unittype == "h1")
        {
            Chart.Plot.XLabel("Hours");
        }
        else if (unittype == "m1")
        {
            Chart.Plot.XLabel("Minutes");
        }
        Chart.Plot.YLabel("Price");
        Chart.Plot.Title("Price over time");
        Chart.Plot.Grid(false);
        Chart.Plot.XAxis.ManualTickPositions(unitdouble, unitlabel );
        
        Chart.Refresh();

        //TODO Make the chart use a time system rather than relying off units so that I can upload a full minute for the past like year
    }
    
}

