namespace MyLinq;

public static class EnumerableExtensions
{
    public static SortableEnumerable<T> MyOrderBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> selector) where TKey : IComparable<TKey>
    {
        return new SortableEnumerable<T>(enumerable).MyThenBy(selector);
    }
}