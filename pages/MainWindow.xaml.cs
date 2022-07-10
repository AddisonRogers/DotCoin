using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DotCoinWPF.pages;

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

        private void NameBox(string nameRaw) //postbox
        {
            var name = nameRaw.ToLower().Trim();
            string[]? names = DotCoinWPF.Fetch.GetAllNames();
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