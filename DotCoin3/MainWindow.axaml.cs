using System;
using System.Collections;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Themes.Fluent;

namespace DotCoin3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Set Frame content
        }
        private void NameBox(string nameRaw) //postbox
        {
            var name = nameRaw.ToLower().Trim();
            string[]? names = Fetch.GetAllNames();
            if (names != null && ((IList)names).Contains(name)) 
            {
                var temp = new Specific
                {
                    id = name
                };
                MVM.Content = temp;


            }
            else
            {
                //TODO go to a "not found" page _ call the specific page
            }
        }
        private void SearchBar_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NameBox(SearchBar.Text);
                SearchBar.Text = "";
            }
        }
    }
}