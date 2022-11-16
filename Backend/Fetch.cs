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
        public static JsonNode Search(string url)
        {
            var cache = Cache.Check(url);
            if (cache != null) return JsonNode.Parse(cache)!;
            
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Bearer-Token", System.IO.File.ReadAllText("api.txt"));
            
            Cache.Log(url);
            
            var result = client.GetStringAsync(url).Result;
            Cache.Log(result);
            
            return JsonNode.Parse(result);
        }
        public static JsonNode GetAll() => Search("https://api.coincap.io/v2/assets")?["data"]; //add error checking

        public static JsonNode? Get(string? symbol)
        {
            var temp = Search($"https://api.coincap.io/v2/assets")["data"];
            for (int i = 0; i < temp.AsArray().Count; i++)
            {
                if (temp[i]["id"].ToString() == symbol) return temp[i];
            }
            return null;
        } 
        public static double GetPrice(string? symbol) => (double)((Get(symbol)?["priceUsd"])!);
        public static string[]? GetAllNames()
        {
            var json = GetAll();
            string?[] names = new string?[json.AsArray().Count];
            for (var i = 0; i < json.AsArray().Count; i++) names[i] = json[i]?["id"]?.ToString();
            return names;
        }
        public static double[] EffHistory(string? id, int dayCount)
        {
            var json = Search($"https://api.coincap.io/v2/assets/{id}/history?interval=d1&start={DateTimeOffset.Now.ToUnixTimeMilliseconds() - 86400000 * dayCount}&end=" + $"{DateTimeOffset.Now.ToUnixTimeMilliseconds()}")["data"];
            var priceList = new double[json!.AsArray().Count];
            for (var i = 0; i != json.AsArray().Count; i++) priceList[i] = double.Parse(json[i]?["priceUsd"]?.ToString()!);
            return priceList;
        }
        public static double? GetRates(string rate = null)
        {
            var json = Search("https://api.coincap.io/v2/rates")?["data"];
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
        public static double[]? GetCryptoMarketCap(int timeValue)
        {
            string[]? nameList = GetAllNames();
            object[] array = new Object[nameList!.Length]; 
            for (int i = 0; i != array.Length; i++) array[i] = new List<double>();
            for(int i = 0; i != array.Length; i++)
            {
                var json = EffHistory(nameList[i], timeValue); 
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
        public static (double[], double[]) MarketOpenClose(string id = "bitcoin", int timeCount = 14) //TODO I NEED TO CACHE THIS
        {
            var history = EffHistory(id, timeCount);
            double[] OpenCloseDiff = new double[history.Length], OpenCloseDifPercent = new double[history.Length];
            for (int i = 1; i < history.Length; i++)
            {
                OpenCloseDiff[i - 1] = history[i] - history[i - 1];
                OpenCloseDifPercent[i-1] = (OpenCloseDiff[i-1]/history[i])*100;
            }
            return (OpenCloseDiff, OpenCloseDifPercent);
        }
    }
}