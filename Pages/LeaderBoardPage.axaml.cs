using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using ScottPlot.Avalonia;

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
        //LeaderBoardChartSet();
        CryptoInfoTextBlockSet();
        
        var dispatcherTimer = new DispatcherTimer();
        dispatcherTimer.Tick += dispatcherTimer_Tick;
        dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
        dispatcherTimer.Start();
    }
    private void dispatcherTimer_Tick(object? sender, EventArgs e)
    {
        //LeaderBoardChartSet();
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

    private void CryptoInfoTextBlockSet()
    {
        
    }

    private void CryptoInfoClicked(object? sender, PointerPressedEventArgs e)
    {
        
    }
}