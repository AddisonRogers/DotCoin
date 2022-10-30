using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading;
using Avalonia.X11;

namespace DotCoin3
{
    public static class Fetch
    {
        public static JsonNode? GetAll() 
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

            switch (interval?.ToCharArray()[0].ToString())
            {
                case "m":
                    timeperiod = DateTimeOffset.Now.ToUnixTimeMilliseconds() - m * int.Parse(interval?.ToCharArray()[1].ToString()!) * timeValue;
                    break;
                case "h":
                    timeperiod = DateTimeOffset.Now.ToUnixTimeMilliseconds() - h * int.Parse(interval?.ToCharArray()[1].ToString()!) * timeValue;
                    break;
                case "d":
                    timeperiod = DateTimeOffset.Now.ToUnixTimeMilliseconds() - d * int.Parse(interval?.ToCharArray()[1].ToString()!) * timeValue;
                    break;
            }

            var json = JsonNode.Parse(client.GetStringAsync($"https://api.coincap.io/v2/assets/{id}/history?interval={interval}&start={timeperiod}&end=" + $"{DateTimeOffset.Now.ToUnixTimeMilliseconds()}").Result)?["data"];
            var priceList = new double[json!.AsArray().Count];
            for (var i = 0; i != json.AsArray().Count; i++) priceList[i] = double.Parse(json[i]?["priceUsd"]?.ToString()!);
            return priceList;
        }
        public static JsonNode? ModHistory(string? id, long timeValue, string? interval)
        {
            using var client = new HttpClient();
            const long d = 86400000;
            const long h = 3600000;
            const long m = 60000;
            long timeperiod = 0;

            switch (interval?.ToCharArray()[0].ToString())
            {
                case "m":
                    timeperiod = DateTimeOffset.Now.ToUnixTimeMilliseconds() - m * int.Parse(interval?.ToCharArray()[1].ToString()!) * timeValue;
                    break;
                case "h":
                    timeperiod = DateTimeOffset.Now.ToUnixTimeMilliseconds() - h * int.Parse(interval?.ToCharArray()[1].ToString()!) * timeValue;
                    break;
                case "d":
                    timeperiod = DateTimeOffset.Now.ToUnixTimeMilliseconds() - d * int.Parse(interval?.ToCharArray()[1].ToString()!) * timeValue;
                    break;
            } //TODO ADD ERROR CHECKING FOR CRYPTOS THAT DONT LAST LONG
            return JsonNode.Parse(client.GetStringAsync($"https://api.coincap.io/v2/assets/{id}/history?interval={interval}&start={timeperiod}&end=" + $"{DateTimeOffset.Now.ToUnixTimeMilliseconds()}").Result)?["data"];
        }
        public static JsonNode? Get(string? symbol)
        {
            using var client = new HttpClient();
            return (JsonNode.Parse(client.GetStringAsync($"https://api.coincap.io/v2/assets?ids={symbol}").Result)?["data"])?[0] as JsonObject;

        }
        public static string[]? GetAllNames()
        {
            var json = GetAll();
            if (json == null) return null;
            string?[] names = new string?[json.AsArray().Count];
            for (var i = 0; i < json.AsArray().Count; i++) names[i] = json[i]?["id"]?.ToString();
            return names;
        }
        public static double? GetRates(string rate = null)
        {
            using var client = new HttpClient();
            var json = JsonNode.Parse(client.GetStringAsync($"https://api.coincap.io/v2/rates").Result)?["data"];
            if (json == null) return null;
            string?[,] rates = new string?[json!.AsArray().Count,2];
            for (var i = 0; i < json.AsArray().Count; i++) {rates[i, 0] = json[i]?["symbol"]?.ToString(); rates[i, 1] = json[i]?["rateUsd"]?.ToString();}
            //if (rate == null) return rates;
            for (var i = 0; i < rates.GetLength(0); i++)
            {
                if (rates[i, 0] == rate) return double.Parse(rates[i, 1]);
            } 
            return null;
        }
        public static string GetFiat() => (new RegionInfo(System.Threading.Thread.CurrentThread.CurrentUICulture.LCID).ISOCurrencySymbol).ToString();
        public static JsonNode? NewsGet(int pages = 0, int offset = 0)
        {
            using var client = new HttpClient();
            if (pages == 0) return JsonNode.Parse(client.GetStringAsync("https://cryptopanic.com/api/v1/posts/?auth_token=95688bf064f757e2cba88fe22e9c1e67e36cdbd1&public=true").Result)?["results"];
            string? json;
            json = (JsonNode.Parse(client.GetStringAsync($"https://cryptopanic.com/api/v1/posts/?auth_token=95688bf064f757e2cba88fe22e9c1e67e36cdbd1&public=true&page={offset}").Result)?["results"])?.ToString();
            json = json?.Remove(json.Length-3,3); // Hello World 
            json = json?.Insert(json.Length, ",\n");
            for (var i = 2 + offset; i < pages + offset -1; i++)
            {
                Thread.Sleep(210);
                string? temp = null;
                temp = (JsonNode.Parse(client.GetStringAsync($"https://cryptopanic.com/api/v1/posts/?auth_token=95688bf064f757e2cba88fe22e9c1e67e36cdbd1&public=true&page={i}").Result)?["results"])?.ToString();
                temp = temp.Remove(0, 3);
                temp = temp.Remove(temp.Length - 3, 3);
                temp = temp.Insert(temp.Length, ",");
                json += temp;
            }
            Thread.Sleep(210);
            json += (JsonNode.Parse(client.GetStringAsync($"https://cryptopanic.com/api/v1/posts/?auth_token=95688bf064f757e2cba88fe22e9c1e67e36cdbd1&public=true&page={pages + offset}").Result)?["results"])?.ToString().Remove(0, 1);
            return JsonNode.Parse(json!);
        }
        public static string?[] NewsGetTitles()
        {
            var json = NewsGet();
            if (json == null) return null;
            string?[] titles = new string?[json.AsArray().Count];
            for (var i = 0; i < json.AsArray().Count; i++) titles[i] = json[i]?["title"]?.ToString();
            return titles;
        } //This is pointless
        public static double[]? GetCryptoMarketCap(long timeValue, string? interval)
        {
            string[]? nameList = GetAllNames();
            object[] array = new Object[nameList!.Length]; 
            for (int i = 0; i != array.Length; i++) array[i] = new List<double>();
            for(int i = 0; i != array.Length; i++)
            {
                var json = History(nameList[i], timeValue, interval); 
                for (var j = 0; j != json.Length; j++) ((List<double>)array[i]).Add(json[j]);
                Thread.Sleep(500);
            }
            double[] arrayadded = new double[array.Length];
            for (int i = 0; i != array.Length; i++) arrayadded[i] = ((List<double>)array[i]).Sum();
            return arrayadded;
            
            
            
            
            
            /* var price = double.Parse(json[0]?["priceUsd"]?.ToString()!);
            var supply = double.Parse(json[0]?["supply"]?.ToString()!);
            return price * supply; /*
        }
        


        /* for getallnames {
         * var jsonid = ModHistory (id);
         * }
         *
         *
         *
         *
         * 
         * array of two numbers
         * abstract = {day/week/month/year, Crypto}
         * effective =
         * {crypto, crypto, crypto} -- Day 1
         * {crypto, crypto, crypto} -- Day 2
         * {crypto, crypto, crypto} -- Day 3
         *
         *
         * Day1.count = new array position 1 etc
         * 
         *
         *
         * 
         * 
         */
            return null;
        }
    }
}