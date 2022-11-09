using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Controls.Shapes;
using Avalonia.Threading;

namespace DotCoin3;

public class Cache
{
    public static void MakeCache()
    {
        System.IO.File.Create("Cache.txt");
        for (int i = 0; i < 2; i++) Log("");
    }
    public static void BinCache() => System.IO.File.Delete("Cache.txt");
    public static void Log(string data)
    {
        using var writer = System.IO.File.AppendText("Cache.txt");
        writer.WriteLine(data);
    }
    public static string? Check(string key)
    {
        var reader = System.IO.File.ReadAllLines("Cache.txt");
        for (int i = 0; i < reader.Length; i++)
            if (reader[i] == key)
            {
                Queue(key);
                return reader[i + 1];
            }
        return null;
    }

    private static void EditCacheNum(int LineNumber, string NewText)
    {
        string[] Read = File.ReadAllLines("Cache.txt");
        Read[LineNumber] = NewText;
        File.WriteAllLines("Cache.txt", Read);
    }

    public static int Find(string[] Array, string Key)
    {
        for (int i = 0; i < Array.Length; i++)
        {
            if (Array[i] == Key) return i;
        }
        return -1;
    }
    private static void EditCacheKey(string Key, string NewText)
    {
        string[] Read = File.ReadAllLines("Cache.txt");
        Read[Find(Read, Key)] = NewText;
        File.WriteAllLines("Cache.txt", Read);
    }

    private static void Update(string Key, string UpdatedText)
    {
        string[] Read = File.ReadAllLines("Cache.txt");
        Read[Find(Read, Key) + 1] = UpdatedText;
        File.WriteAllLines("Cache.txt", Read);
    }
    private static void Queue(string key)
    {
        string[] Read = File.ReadAllLines("Cache.txt");
        if (Read[0] != "") Read[0] += "#DOT#";
        Read[0] += key;
        File.WriteAllLines("Cache.txt", Read);
    }
    
    private static void DeQueue(string key)
    {
        string[] Read = File.ReadAllLines("Cache.txt"), Queues = Read[0].Split("#DOT#");
        string New0 = "";
        for (int i = 0; i < Queues.Length - 1; i++)
        {
            if (i != 0) New0 += "#DOT#";
            New0 += Queues[i];
        }
        Read[0] = New0;
        File.WriteAllLines("Cache.txt", Read);
    }
    public static void UpdateValue(string key)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Bearer-Token", System.IO.File.ReadAllText("api.txt"));
        Update(key, client.GetStringAsync(key).Result);
        DeQueue(key);
    }
    public void Hub()
    {
        var dispatcherTimer = new DispatcherTimer();
        dispatcherTimer.Tick += DispatcherTimerOnTick;
        dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
        dispatcherTimer.Start();
    }

    private void DispatcherTimerOnTick(object? sender, EventArgs e) => UpdateValue(File.ReadAllLines("Cache.txt")[0].Split("#DOT#")[^1]);
}