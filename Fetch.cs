using System.Net.Http.Json;

namespace DotCoinWPF;

using System;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

public static class Fetch
{
    private static JsonNode? GetAll()
    {
        using HttpClient client = new HttpClient();
        return JsonNode.Parse(client.GetStringAsync("https://api.coincap.io/v2/assets").Result)?["data"];
    }
    public static double[] History(string? id, int timeValue, string? interval)
        {
            using HttpClient client = new HttpClient();
            const int d = 86400000; const int h = 3600000; const int m = 60000;
            long timeperiod = 0;
            
            
            if (interval?.ToCharArray()[0].ToString() == "m") timeperiod = (DateTimeOffset.Now.ToUnixTimeMilliseconds() - (m * int.Parse(interval?.ToCharArray()[1].ToString()!) * timeValue));
            else if (interval?.ToCharArray()[0].ToString() == "h") timeperiod = (DateTimeOffset.Now.ToUnixTimeMilliseconds() - (h * int.Parse(interval?.ToCharArray()[1].ToString()!) * timeValue));
            else if (interval?.ToCharArray()[0].ToString() == "d") timeperiod = (DateTimeOffset.Now.ToUnixTimeMilliseconds() - ((d * int.Parse(interval?.ToCharArray()[1].ToString()!)) * timeValue));
            else Console.WriteLine("Help");
            JsonNode? json = null;
            while (json == null){json = JsonNode.Parse(client.GetStringAsync($"https://api.coincap.io/v2/assets/{id}/history?interval={interval}&start={timeperiod}&end=" + $"{DateTimeOffset.Now.ToUnixTimeMilliseconds()}").Result)?["data"];}
            //JsonNode? json = JsonNode.Parse(client.GetStringAsync($"https://api.coincap.io/v2/assets/{id}/history?interval={interval}&start={timeperiod}&end=" + $"{DateTimeOffset.Now.ToUnixTimeSeconds()}").Result)?["data"];
            double[] priceList = new double[json.AsArray().Count];
            for (int i = 0; i != json.AsArray().Count; i++)
            {
                priceList[i] = double.Parse(json[i]?["priceUsd"]?.ToString()!);
            }

            return priceList;
        }
        public static JsonNode? Get(string? symbol)
        {

            JsonNode? json = GetAll();
            if (json != null)
            {
                var count = json.AsArray().Count;
                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (json[i]?["id"]?.ToString() == symbol)
                        {
                            return json[i];
                        }

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }

            }

            return null; //error checking
        }
        public static string[]? GetAllNames()
        {
            JsonNode? json = GetAll();
            if (json == null) return null;
            
            string?[] names = new string?[json.AsArray().Count];
            for (int i = 0; i < json.AsArray().Count; i++)
            {

                names[i] = json[i]?["id"]?.ToString();


            }

            return names;
        }
            
}