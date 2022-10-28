using System.Text.Json.Nodes;

namespace DotCoin3;

public class Sort
{ //So this can easily be changed into a better reusable class but for the purpose of my use case I am only sorting stuff once.
    //https://www.crio.do/blog/top-10-sorting-algorithms/
    
    
    //Really basic usage is
    //ToSort = The JsonNode you want sorted
    //type = the identifier of how you want it sorted ie Price or Rank
    private JsonNode Swap(JsonNode ToSwap, int i, int j)
    {
        (ToSwap[i], ToSwap[j]) = (ToSwap[j], ToSwap[i]);
        return ToSwap;
    }
    private JsonNode? SelectionSort(JsonNode ToSort, string type)
    {
        /*
         * n = len(a)
         * for i = 1 to n {
         *     for j = i+1 to n {
         *         if a[i] > a[j] {
         *              swap a[i] and a[j]
         */
        
        int n = (ToSort.AsArray().Count);
        for (int i = 0; i <= n; i++)
        {
            for (int j = i; j <= n; j++)
            {
                double a,b;
                if (type == "A-Z") { a = ToSort[i]["id"].ToString().CompareTo(ToSort[j]["id"].ToString()); b = ToSort[j]["id"].ToString().CompareTo(ToSort[i]["id"].ToString()); } else { a = (int)ToSort[i][type]; b = (int)ToSort[j][type]; }
                if (a > b) ToSort = Swap(ToSort, i, j);
            }
        }
        return ToSort;
    }
    private JsonNode? InsertionSort(JsonNode? ToSort, string type)
    {
        /*
         * n = len(a)
         * for i = 1 to n {
         *     for j = 1 to i {
         *         if a[i] < a[j] {
         *              swap a[i] and a[j]
         */
        
        int n = (ToSort.AsArray().Count);
        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                double a,b;
                if (type == "A-Z") { a = ToSort[i]["id"].ToString().CompareTo(ToSort[j]["id"].ToString()); b = ToSort[j]["id"].ToString().CompareTo(ToSort[i]["id"].ToString()); } else { a = (int)ToSort[i][type]; b = (int)ToSort[j][type]; }
                if (a < b) ToSort = Swap(ToSort, i, j);
            }
        }
        return ToSort;
        
    }
    private JsonNode? SimpleSort(JsonNode? ToSort, string type)
    {
        /*
         * n = len(a)
         * for i = 1 to n {
         *     for j = 1 to n {
         *         if a[i] < a[j] {
         *              swap a[i] and a[j]
         */
        
        int n = (ToSort.AsArray().Count);
        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j <= n; j++)
            {
                double a,b;
                if (type == "A-Z") { a = ToSort[i]["id"].ToString().CompareTo(ToSort[j]["id"].ToString()); b = ToSort[j]["id"].ToString().CompareTo(ToSort[i]["id"].ToString()); } else { a = (int)ToSort[i][type]; b = (int)ToSort[j][type]; }
                if (a < b) ToSort = Swap(ToSort, i, j);
            }
        }
        return ToSort;
        
    }
    
    
    private JsonNode BubbleSort(JsonNode ToSort, string type)
    {
        return null;
    }
    private JsonNode? MergeSort(JsonNode? ToSort, string type)
    {
        //fast rabbit type
        return null;
    }
    private JsonNode? QuickSort(JsonNode? ToSort, string type)
    {
        return null;
    }
    private JsonNode? HeapSort(JsonNode? ToSort, string type)
    {
        return null;
    }
    
    private JsonNode? RadixSort(JsonNode? ToSort, string type)
    {
        return null;
    }
}