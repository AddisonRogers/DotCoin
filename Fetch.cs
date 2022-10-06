using System;
using System.Globalization;
using System.Net.Http;
using System.Text.Json.Nodes;

namespace DotCoin3;

public static class Fetch
{
    private static JsonNode? GetAll()
    {
        using var client = new HttpClient();
        return JsonNode.Parse(client.GetStringAsync("https://api.coincap.io/v2/assets").Result)?["data"]; //add error checking
    }

    public static double[] History(string? id, long timeValue, string? interval)
    {
        using var client = new HttpClient();
        const long d = 86400000;
        const long h = 3600000;
        const long m = 60000;
        long timeperiod = 0;

        if (interval?.ToCharArray()[0].ToString() == "m")
            timeperiod = DateTimeOffset.Now.ToUnixTimeMilliseconds() -
                         m * int.Parse(interval?.ToCharArray()[1].ToString()!) * timeValue;
        else if (interval?.ToCharArray()[0].ToString() == "h")
            timeperiod = DateTimeOffset.Now.ToUnixTimeMilliseconds() -
                         h * int.Parse(interval?.ToCharArray()[1].ToString()!) * timeValue;
        else if (interval?.ToCharArray()[0].ToString() == "d")
        {
            timeperiod = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            long egg = (d * long.Parse(interval?.ToCharArray()[1].ToString()!) * timeValue);
            timeperiod -= egg;
        }
        else Console.WriteLine("Help");
        var json = JsonNode.Parse(client
            .GetStringAsync(
                $"https://api.coincap.io/v2/assets/{id}/history?interval={interval}&start={timeperiod}&end=" +
                $"{DateTimeOffset.Now.ToUnixTimeMilliseconds()}").Result)?["data"];
        var priceList = new double[json!.AsArray().Count];
        for (var i = 0; i != json.AsArray().Count; i++) priceList[i] = double.Parse(json[i]?["priceUsd"]?.ToString()!);
        return priceList;
    }

    public static JsonNode? Get(string? symbol)
    {
        using var client = new HttpClient();
        return (JsonNode.Parse(client.GetStringAsync($"https://api.coincap.io/v2/assets/{symbol}").Result)?["data"])?[0] as JsonObject;
    }

    public static string[]? GetAllNames()
    {
        var json = GetAll();
        if (json == null) return null;
        string?[] names = new string?[json.AsArray().Count];
        for (var i = 0; i < json.AsArray().Count; i++) names[i] = json[i]?["id"]?.ToString();
        return names;
    }
    public static string?[,] GetRates(string rate = null)
    {
        using var client = new HttpClient();
        var json = JsonNode.Parse(client.GetStringAsync($"https://api.coincap.io/v2/rates").Result)?["data"];
        if (json == null) return null;
        string?[,] rates = new string?[json!.AsArray().Count,2];
        for (var i = 0; i < json.AsArray().Count; i++) {rates[i, 0] = json[i]?["symbol"]?.ToString(); rates[i, 1] = json[i]?["rateUsd"]?.ToString();}
        if (rate == null) return rates;
        for (var i = 0; i < rates.Length; i++) { if (rates[i, 0] == rate) return new string?[,] {{rates[i, 0], rates[i, 1]}}; }
        return null;
    }
    public static string GetFiat()
    {
        var ri = new RegionInfo(System.Threading.Thread.CurrentThread.CurrentUICulture.LCID);
        return ri.ISOCurrencySymbol;
    }
    
    
}