namespace DotCoinWPF;

using System;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;


public static class Fetch
{
    public static async Task<JsonNode?> GetAll()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://api.coincap.io/v2/assets");
            return JsonNode.Parse(await response.Content.ReadAsStringAsync())?["data"];
        }
    
        public static async Task<double[]?> History(string? id, int timeValue, string? interval)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://api.coincap.io/v2/assets/{id}/history?interval={interval}");
            var json = JsonNode.Parse(await response.Content.ReadAsStringAsync())?["data"];
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

            JsonNode? json = await GetAll();
            if (json != null)
            {
                var count = json.AsArray().Count;
                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (json[i]?["symbol"]?.ToString() == symbol)
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
            JsonNode? json = await GetAll();
            if (json == null) return null;
            
            string?[] names = new string?[json.AsArray().Count];
            for (int i = 0; i < json.AsArray().Count; i++)
            {

                names[i] = json[i]?["id"]?.ToString();


            }

            return names;
        }
            
}