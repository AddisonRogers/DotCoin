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
    
        public static async Task<double[]> History(string? id, int timeValue, string? interval)
        {
            HttpClient client = new HttpClient();
            
            Task<string> t = client.GetStringAsync($"https://api.coincap.io/v2/assets/{id}/history?interval={interval}");
            t.Wait();
            JsonNode? json = JsonNode.Parse(t.Result)?["data"];
            
            //var json = JsonNode.Parse(response.Content.ReadAsStringAsync().Result)?["data"];
            
                var count = json.AsArray().Count -1;
            
                double[] prices = new double[timeValue];
                int x = count - timeValue;
                int counter = -1;
                for (int i = count; i > x; i--)
                {
                    counter++;
                    prices[counter] = double.Parse(json[i]?["priceUsd"]?.ToString() ?? string.Empty);
                }
            
                return prices; //needs to be relooked and tested
            
        
        }
        public static async Task<JsonNode?> Get(string? symbol)
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
        public static async Task<string[]?> GetAllNames()
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