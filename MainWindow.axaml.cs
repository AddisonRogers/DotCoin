using System;
using System.Collections;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Themes.Fluent;
using DotCoin3.Pages;

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
                    Id = name
                };
                this.Find<UserControl>("MVM").Content = temp;
            }
            else
            {
                if (names == null)
                {
                    
                }
                if (!((IList)names).Contains(name))
                {
                    var temp = new error_page()
                    {
                        error_code = "404 - Could not find the coin you were looking for",
                    };
                    this.Find<UserControl>("MVM").Content = temp;
                }
            }
        }
        private void SearchBar_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NameBox(this.Find<TextBox>("SearchBar").Text);
                this.Find<TextBox>("SearchBar").Text = "";
                
            }
        }
        private void StyledElement_OnInitialized(object? sender, EventArgs e)
        {
            var temp = new LeaderBoardPage(){};
            
            MVM.Content = temp;
            //this.Find<UserControl>("MVM").Content = temp;
        }
    }
}