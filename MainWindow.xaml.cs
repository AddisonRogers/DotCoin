using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DotCoinWPF;
using System.Linq;

namespace DotCoinWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new Page1();
        }

        private async Task NameBox(string nameRaw) //postbox
        {
            var name = nameRaw.ToLower().Trim();
            string[]? names = await DotCoinWPF.Fetch.GetAllNames();
            if (names != null && names.Contains(name))
            {
                MainFrame.Content = new Page2(name); //TODO if name isnt in list go to a "not found" page
            }
        }

        private void SearchBar_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NameBox(SearchBar.Text);
                SearchBar.Text= "";
            }
        }
    }
}