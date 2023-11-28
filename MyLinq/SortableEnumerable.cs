using System.Collections;

namespace MyLinq;

public class SortableEnumerable<T> : IEnumerable<T>
{
    private readonly IEnumerable<T> _elements;
    private readonly LinkedList<Func<T, IComparable>> _sortSelectors = new();

    public SortableEnumerable(IEnumerable<T> elements)
    {
        _elements = elements;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var list = Materialize();
        return list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public List<T> ToList()
    {
        return Materialize();
    }
    
    public SortableEnumerable<T> MyThenBy(Func<T, IComparable> selector)
    {
        _sortSelectors.AddLast(selector);
        return this;
    }

    public SortableEnumerable<T> MyOrderBy(Func<T, IComparable> selector)
    {
        //предположение - OrderBy(A).OrderBy(B) = ThenBy(B).ThenBy(A)
        _sortSelectors.AddFirst(selector);
        return this;    
    }

    private List<T> Materialize()
    {
        var list = _elements.ToList();
        
        list.Sort((a, b) =>
        {
            foreach (var selector in _sortSelectors)
            {
                var keyA = selector(a);
                var keyB = selector(b);

                var comparisonResult = keyA.CompareTo(keyB);
                if (comparisonResult != 0)
                {
                    return comparisonResult;
                }
            }

            return 0;
        });

        return list;
    }
}