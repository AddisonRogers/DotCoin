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
        
        public static double? MovingAverage(string? id, int daycount = 14)
        {
            var prices = Fetch.EffHistory(id, daycount);
            return prices.Sum() / prices.Length;
        }
        public static double? EMA(string? id = "bitcoin", int daycount = 20)
        { 
            if (daycount != 1)
            {
                var partone = Fetch.EffHistory(id, daycount)?[0];
                var parttwo = (2 / (daycount + 1));
                var partthree = (EMA(id, daycount - 1) ?? 0) * (1 - (2 / (daycount + 1)));
                return (partone * parttwo + partthree);
            }
            return 0;
        } 
        public static double MACD(string? id = "bitcoin") => (double)(EMA(id, 12) - EMA(id, 26));
        public static double High(string? id = "bitcoin", int daycount = 14) => Fetch.EffHistory(id, daycount).Max();
        public static double Low(string? id = "bitcoin", int daycount = 14) => Fetch.EffHistory(id, daycount).Min();
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
        //public static double MA
        public static (double? BOLU, double? BOLD) BollingerBands(string? id = "bitcoin", int daycount = 20)
        {
            var SMA = MovingAverage(id, daycount);
            var SD = StandardDeviation(id, daycount);
            return ((SMA + SD * 2), (SMA - SD * 2));
        }
        
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
        
        public static (double conversionline, double baseline, double leadingA, double leadingB) IchimokuCloud(string? id = "bitcoin", int daycount = 14)
        {
            var high = High(id, daycount);
            var low = Low(id, daycount);

            var conversionline = ((9 - high) + (9 - low)) / 2;
            var baseline = ((26 - high) + (26 - low)) / 2;

            var leadingA = (conversionline + baseline) / 2;
            var leadingB = ((52 - high) + (52 - low)) / 2;

            return (conversionline, baseline, leadingA, leadingB); 
        }
        /*
         * Price > Cloud = Price is going up
         * Price < Cloud = Price is going down
         * When A > B = Uptrend
         * When B > A = Downtrend
         */
        public static double StandardDeviation(string? id = "bitcoin", int daycount = 14)
        {
            double[] history = Fetch.EffHistory(id, daycount);
            double historyavg = history.Average(), historyvarsqsum = 0;
            for (int i = 0; i < history.Length; i++) historyvarsqsum += (history[i] - historyavg) * (history[i] - historyavg);
            return Math.Sqrt(historyvarsqsum / history.Length - 1);
        }
        //Volume weighted average price
        public static double VolumeWeightedAverage(string? id = "bitcoin") => (double)Fetch.Get(id)?["vwap24Hr"]!;
        
    }
}
