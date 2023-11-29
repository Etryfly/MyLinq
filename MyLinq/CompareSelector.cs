namespace MyLinq;

public class CompareSelector<T, TKey> : ICompareSelector<T> where TKey : IComparable<TKey>
{
    private readonly Func<T, TKey> _selector;

    public CompareSelector(Func<T, TKey> selector)
    {
        _selector = selector;
    }

    public int Compare(T a, T b)
    {
        var keyA = _selector(a);
        var keyB = _selector(b);

        return keyA.CompareTo(keyB);
    }
}