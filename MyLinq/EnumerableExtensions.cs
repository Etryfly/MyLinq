namespace MyLinq;

public static class EnumerableExtensions
{
    public static SortableEnumerable<T> MyOrderBy<T>(this IEnumerable<T> enumerable, Func<T, IComparable> selector)
    {
        return new SortableEnumerable<T>(enumerable).MyThenBy(selector);
    }
}