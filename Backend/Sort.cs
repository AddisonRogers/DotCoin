using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace DotCoin3;

public class Sort
{ 
    private static void Swap(Dictionary<string, string>[] ToSwap, int i, int j) => (ToSwap[i], ToSwap[j]) = (ToSwap[j], ToSwap[i]);
    private static (double, double) GetAB(Dictionary<string, string>[] ToSort, int i, int j, string type)
    { 
        if (type == "A-Z")
        {
            return (
                String.Compare(ToSort[i]["id"], ToSort[j]["id"], StringComparison.Ordinal),
                String.Compare(ToSort[j]["id"], ToSort[i]["id"], StringComparison.Ordinal)
            );
        }
        var a = double.Parse(ToSort[i][type]); 
        var b = double.Parse(ToSort[j][type]);
        return (a,b);
    }
    public static Dictionary<string, string>[] Convert(JsonNode ToConvert)
    {
        //forloop to get the length of the array
        Dictionary<string, string>[] objects = new Dictionary<string, string>[ToConvert.AsArray().Count];
        for (int i = 0; i < ToConvert.AsArray().Count; i++)
        {
            //This runs for all the different items in the JSON Node meaning this is where we run our second for loop for all the items in the json array
            objects[i] = new Dictionary<string, string>();
            
            for (int j = 0; j < ToConvert[i].AsObject().Count; j++)
            {
                //ToConvert[i][j].AsObject();
                string[] keys = {
                    "id",
                    "rank",
                    "symbol",
                    "name",
                    "supply",
                    "maxSupply",
                    "marketCapUsd",
                    "volumeUsd24Hr",
                    "priceUsd",
                    "changePercent24Hr",
                    "vwap24Hr",
                    "explorer"
                };
                if (ToConvert[i][keys[j]] != null) { objects[i].Add(keys[j], ToConvert[i][keys[j]].ToString()); 
                } else { objects[i].Add(keys[j], "null"); }
            }
        }
        return objects;
        //returning an array of: array of dictionaries
    }
    public static Dictionary<string, string>[] SelectionSort(JsonNode ToSortJSON, string type)
    {
        /*
         * for i = 1 to n {
         *     for j = i+1 to n {
         *         if a[i] > a[j] {
         *              swap a[i] and a[j]
         */
        var ToSort = Convert(ToSortJSON);
        int n = (ToSort.Length -1);
        for (int i = 0; i <= n; i++)
        {
            for (int j = i; j <= n; j++)
            {
                double a,b;
                (a,b) = GetAB(ToSort, i, j, type);
                if (a > b) Swap(ToSort, i, j);
            }
        }
        return ToSort;
    }
    public static Dictionary<string, string>[] InsertionSort(JsonNode? ToSortJSON, string type)
    {
        /*
         * for i = 1 to n {
         *     for j = 1 to i {
         *         if a[i] < a[j] {
         *              swap a[i] and a[j]
         */
        var ToSort = Convert(ToSortJSON);
        int n = (ToSort.Length);
        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                double a,b;
                (a,b) = GetAB(ToSort, i, j, type);                
                if (a < b) Swap(ToSort, i, j);
            }
        }
        return ToSort;
        
    }
    public static Dictionary<string, string>[] SimpleSort(JsonNode? ToSortJSON, string type)
    {
        var ToSort = Convert(ToSortJSON);
        int n = (ToSort.Length);
        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j <= n; j++)
            {
                double a,b;
                (a,b) = GetAB(ToSort, i, j, type);
                if (a < b) Swap(ToSort, i, j);
            }
        }
        return ToSort;
    } 
    public static Dictionary<string, string>[] BubbleSort(JsonNode ToSortJSON, string type)
    {
        var ToSort = Convert(ToSortJSON);
        
        for (int i = 1; i <= ToSort.Length - 1; ++i) {
            for (int j = 0; j < ToSort.Length - i; ++j)
            {
                double a, b;
                (a, b) = GetAB(ToSort, j, j + 1, type);
                if (a > b) Swap(ToSort, j, j+1);
            }
        }
        return ToSort;
    }
}