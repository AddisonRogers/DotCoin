using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;

namespace DotCoinWPF.pages
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2(string name)
        {
            InitializeComponent();
            refreshcoin(name);
            

        }
        private async void refreshcoin (string name)
        {
            var coin = await Fetch.Get(name);
            Title = name;
            SymbolBox.Text = "Symbol : "+coin?["symbol"]?.ToString();
            NameBox.Text = "Name : "+coin?["name"]?.ToString();
            SupplyBox.Text = "Circulating Supply : " + Math.Round(double.Parse(coin?["supply"]?.ToString() ?? String.Empty),0);
            if (coin?["maxSupply"]?.ToString() != null)
            {
                MaxSupplyBox.Text = "Max Supply : " + Math.Round(double.Parse(coin?["maxSupply"]?.ToString() ?? String.Empty), 0);
            }
            else
            {
                MaxSupplyBox.Text = "Max Supply : " + "N/A";
            }
            MarketCapBox.Text = "Market Cap : "+ Math.Round(double.Parse(coin?["marketCapUsd"]?.ToString() ?? string.Empty),0);
            PriceBox.Text = "Price : $"+ Math.Round(double.Parse(coin?["priceUsd"]?.ToString() ?? string.Empty), 2);
            Change24HBox.Text = "Volume change in 24h : "+Math.Round(double.Parse(coin?["volumeUsd24Hr"]?.ToString() ?? string.Empty), 0);
            ChangeP24HBox.Text = "% Change in 24h : "+Math.Round(double.Parse(coin?["changePercent24Hr"]?.ToString() ?? string.Empty), 1)+"%"; //TODO should probably redo this and make it more complex etc. (Also add a graph)
            
            
            
            
            
            
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            
            
            DispatcherTimer dispatcherTimer = new DispatcherTimer();  
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);  
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);  
            dispatcherTimer.Start();  
        }  
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            refreshcoin(Title);
        }
        
        
    
}
}
