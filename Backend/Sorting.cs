namespace DotCoin3;

public class Sorting
{
    public object Sort(string type = "Price", object ToSort = null, string SortType = "Linear")
    {
        if (ToSort == null)
        {
            return null;
        }
        else
        {
            switch (SortType)
            {
                case "Linear":
                    return LinearSort(ToSort, type);
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
                    break;
            }
        }
    }
    private object LinearSort(object ToSort, string type)
    {
        return null;
    }
    private object BubbleSort(object ToSort, string type)
    {
        return null;
    }
    private object MergeSort(object ToSort, string type)
    {
        return null;
    }
    private object QuickSort(object ToSort, string type)
    {
        return null;
    }
    private object HeapSort(object ToSort, string type)
    {
        return null;
    }
    private object InsertionSort(object ToSort, string type)
    {
        return null;
    }
    private object RadixSort(object ToSort, string type)
    {
        return null;
    }
    
}