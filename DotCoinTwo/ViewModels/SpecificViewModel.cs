using System;
using Avalonia.Threading;

namespace DotCoinTwo.ViewModels;

public class SpecificViewModel : ViewModelBase
{
    public SpecificViewModel(string id)
    {
        // This is the constructor
        Console.WriteLine(id);
        string symbol;
        string name;
        string circulatingsupply;
        string maxsupply;
        string marketcap;
        string price;
        string change24h;
        string changep24h;

        refreshcoin(id);
        var dispatcherTimer = new DispatcherTimer();
        dispatcherTimer.Tick += (sender, e) => refreshcoin(id);
        dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
        dispatcherTimer.Start();
        
    }
    public void refreshcoin(string id)
    {
        var coin = Fetch.Get(id);
        
    }

    
}
