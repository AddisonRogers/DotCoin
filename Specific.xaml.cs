using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DotCoinWPF
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

        public async void refreshcoin (string name)
        {
            
            var coin = await Fetch.Get(name);
            Title = "DotCoin | "+name+" "+coin?["priceUsd"]?.ToString();
            SymbolBox.Text = "Symbol : "+coin?["symbol"]?.ToString();
            NameBox.Text = "Name : "+coin?["name"]?.ToString();
            SupplyBox.Text = "Circulating Supply : " + Math.Round(double.Parse(coin?["supply"]?.ToString() ?? String.Empty),0);
            
            //MaxSupplyBox.Text = "Max Supply : " + Math.Round(double.Parse(coin?["maxSupply"]?.ToString() ?? String.Empty),0);
            if (coin?["maxSupply"]?.ToString() != null)
            {
                MaxSupplyBox.Text = "Max Supply : " + Math.Round(double.Parse(coin?["maxSupply"]?.ToString() ?? String.Empty), 0);
            }
            else
            {
                MaxSupplyBox.Text = "Max Supply : " + "N/A";
            }
            
            
            //Math.Round(double.Parse(coin?["maxSupply"]?.ToString() ?? String.Empty),0);
            
            MarketCapBox.Text = "Market Cap : "+ Math.Round(double.Parse(coin?["marketCapUsd"]?.ToString() ?? string.Empty),0);
            PriceBox.Text = "Price : $"+ Math.Round(double.Parse(coin?["priceUsd"]?.ToString() ?? string.Empty), 2);
            
            Change24HBox.Text = "Volume change in 24h : "+Math.Round(double.Parse(coin?["volumeUsd24Hr"]?.ToString() ?? string.Empty), 0);
            ChangeP24HBox.Text = "% Change in 24h : "+Math.Round(double.Parse(coin?["changePercent24Hr"]?.ToString() ?? string.Empty), 1)+"%";
            
        }
        
    }
}
