using System;
using System.Linq;
using System.Text.Json.Nodes;

namespace DotCoin3
{
    public class Indicator
    {

    
        //https://www.ig.com/en/trading-strategies/10-trading-indicators-every-trader-should-know-190604


        //Moving average ((Since crypto crash) Days)
        //then cut off after 16th of june as thats after the latest crash
        //double unixTimestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        public static double? MovingAverage(string? id = "bitcoin")
        {
            var timeValue = (DateTime.Today - new DateTime(2022, 6, 16)).Days;
            var prices = Fetch.History(id, timeValue, "d1");
            return prices.Sum() / prices.Length;
        }
        //TODO TODAY

        //Exponential moving average (12-26 Days)
        public static (double? SMA, JsonNode History) SMA(string? id = "bitcoin", int daycount = 20)
        {
            var History = Fetch.ModHistory(id, daycount, "d1");
            double[] countlist = new double[] { };
            for (int i = 1; i < History.AsArray().Count; i++) countlist[i] = (double)(History[i]?["priceUsd"] ?? 0); 
            return (countlist.Sum()/History.AsArray().Count, History);
        }

        public static double? EMA(string? id = "bitcoin", int daycount = 20)
        {
            if (daycount != 0) return ((double)(Fetch.ModHistory(id, daycount, "d1")?[0]?["priceUsd"] ?? 1) * (2 / (daycount + 1) + (EMA(id, daycount-1) ?? 0) * (1 -(2 / (daycount + 1)))));
            return 0;
        } 
        
        //Stochastic oscillator (General trend?)

        //MACD (12-26 Days)

        //Bollinger bands (20 Days)

        //RSI (14 Days)

        //Fibonacci retracement

        //Ichimoku cloud

        //Standard Deviation

        //Average Directional Index

        //Cumulative Volume Delta

        //Volume weighted average price
    }
}
