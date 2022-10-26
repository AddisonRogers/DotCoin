using System.Text.Json.Nodes;

namespace DotCoin3;

public class Sorting
{
    public JsonNode? Sort(string type = "Price", JsonNode ToSort = null, string SortType = "Linear")
    {
        if (ToSort == null)
        {
            return null;
        }
        else
        {
            switch (SortType)
            {
                case "Selection":
                    return SelectionSort(ToSort, type);
                case "Bubble":
                    return BubbleSort(ToSort, type);
                case "Merge":
                    return MergeSort(ToSort, type);
                case "Quick":
                    return QuickSort(ToSort, type);
                case "Heap":
                    return HeapSort(ToSort, type);
                case "Insertion":
                    return InsertionSort(ToSort, type);
                case "Radix":
                    return RadixSort(ToSort, type);
                default:
                    return null;
                    break;
            }
        }
    }
    //https://www.crio.do/blog/top-10-sorting-algorithms/
    private JsonNode? SelectionSort(JsonNode? ToSort, string type)
    {
        return null;
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
    private JsonNode? InsertionSort(JsonNode? ToSort, string type)
    {
        return null;
    }
    private JsonNode? RadixSort(JsonNode? ToSort, string type)
    {
        return null;
    }
    
}