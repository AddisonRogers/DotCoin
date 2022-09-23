using System;
using System.Linq;

namespace DotCoinTwo;

public class Indicator
{
    //https://www.ig.com/en/trading-strategies/10-trading-indicators-every-trader-should-know-190604
    
    //Moving average ((Since crypto crash) Days)
    //then cut off after 16th of june as thats after the latest crash
    //double unixTimestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    public static double? MovingAverage(string? id)
    {
        var timeValue = (DateTime.Today - new DateTime(2022, 6, 16)).Days;
        var prices = Fetch.History(id, timeValue, "d1");
        return prices.Sum() / prices.Length;
    }

    //Exponential moving average (12-26 Days)
    
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