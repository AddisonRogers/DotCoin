using System.Threading.Tasks;

namespace DotCoinWPF;
using System;

public class Program
{
    public static async Task main()
    {
        //https://api.coincap.io/v2/assets is the api site for the coin data

//Console.WriteLine(await Fetch.GetAll());


        Console.WriteLine("Which coin would you like to know about?");
        var coinSymbol = "BTC";
        var coinFlag = false;
        while (coinFlag == false)
        {
            coinSymbol = Console.ReadLine()?.ToUpper().Trim();
            if ((coinSymbol == null) | (await Fetch.Get(coinSymbol) == null))
            {
                Console.WriteLine("Please enter a valid coin");

            }
            else
            {
                coinFlag = true;
            }


        }

        var coin = await Fetch.Get(coinSymbol);
        double price = double.Parse(coin?["priceUsd"]?.ToString() ?? string.Empty);
        string name = coin?["id"]?.ToString().Trim().ToLower() ?? string.Empty;

//Console.WriteLine($"The price of {name} is {price}");
//double[]? prices = (await Fetch.History(name, 5, "h1"));
//foreach (double i in prices){Console.WriteLine(i);}
        Console.WriteLine(await Indicator.MovingAverage(name));
    }
}