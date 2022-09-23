using System.Collections;
using Avalonia.Controls;
using Avalonia.Input;

namespace DotCoinTwo.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void NameBox(string nameRaw) //postbox
        {
            var name = nameRaw.ToLower().Trim();
            string[]? names = Fetch.GetAllNames();
            if (names != null && ((IList)names).Contains(name)) ;
            //TODO if name isnt in list go to a "not found" page _ call the specific page
        }

        private void SearchBar_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            NameBox(SearchBar.Text);
            SearchBar.Text = "";
        }
    }
}