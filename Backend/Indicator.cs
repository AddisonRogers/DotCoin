using System;
using System.Collections.Generic;
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
        public static (double? SMA, JsonNode History) SMA(string? id = "bitcoin", int daycount = 20)
        {
            var History = Fetch.ModHistory(id, daycount, "d1") ?? throw new ArgumentNullException();
            double[] countlist = new double[] { };
            for (int i = 1; i < History.AsArray().Count; i++) countlist[i] = (double)(History[i]?["priceUsd"] ?? 0); 
            return (countlist.Sum()/History.AsArray().Count, History);
        }
        public static double? EMA(string? id = "bitcoin", int daycount = 20)
        {
            if (daycount != 0) return ((double)(Fetch.ModHistory(id, daycount, "d1")?[0]?["priceUsd"] ?? 1) * (2 / (daycount + 1) + (EMA(id, daycount-1) ?? 0) * (1 -(2 / (daycount + 1)))));
            return 0;
        } //TODO make this more efficent
        public static double MACD(string? id = "bitcoin") => (double)(EMA(id, 12) - EMA(id, 26));
        public static double High(string? id = "bitcoin", int daycount = 14) => Fetch.History(id, daycount, "d1").Max();
        public static double Low(string? id = "bitcoin", int daycount = 14) => Fetch.History(id, daycount, "d1").Min();
        public static double TP(string? id = "bitcoin", int daycount = 14) => (High(id, daycount) + Low(id, daycount) + Fetch.GetPrice(id)) / 3;
        public static int StochasticOscillator(string? id = "bitcoin") => (int)(Fetch.GetPrice(id) - Low(id, 14) / High(id, 14) - Low(id, 14)) * 100;
        //Stochastic oscillator (General trend?)
        /*
         * C = the most recent closing
         * L14 Lowest price traded of the 14 previous trading sessions
         * H14 the highest price traded during hte same 14 day period
         * K = (C-L14/H14-L14)*100
         * If it is above 80 sell
         * If it is below 20 buy
         */

        //Bollinger bands (20 Days)
        /*
         * Upper bands and lower bands btw
         * not sure how i should indicate this but yk potato potato
         */
        // public static (double, double) BollingerBands(string? id = "bitcoin", ) TODO later when standard deviation is done
        
        //RSI (14 Days)
        
        /*
         * So I have to get the market closes and opens (ie the % diff since the last one) - Recursion
         * -%2, 3% etc
         * then I do 100 - (100/1+((3/n)/(2/n)))
         */
        public static double RSI(string? id = "bitcoin", int daycount = 14)
        {
            var (_ , OC) = Fetch.MarketOpenClose(id, daycount);
            // Splitting negative and positive
            List<double> Pos = new List<double>();
            List<double> Neg = new List<double>(); // I dont like working in lists as it's memory
            for (int i = 0; i < OC.Length; i++)
            {
                if (OC[i] >= 0) Pos.Add(OC[i] );
                if (OC[i]  < 0) Neg.Add(Math.Abs(OC[i] ));
            }
            return 100 - (100 / (1 + ( (Pos.Average() / daycount) / (Neg.Average() / daycount) ) ));
        }
        //Fibonacci retracement

        //Ichimoku cloud

        //Standard Deviation

        //Average Directional Index

        //Cumulative Volume Delta

        //Volume weighted average price
    }
}
