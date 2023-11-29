using System.Collections;

namespace MyLinq;

public class SortableEnumerable<T> : IEnumerable<T>
{
    private readonly IEnumerable<T> _elements;
    private readonly LinkedList<ICompareSelector<T>> _sortSelectors = new();

    public SortableEnumerable(IEnumerable<T> elements)
    {
        _elements = elements.ToList();
    }

    private SortableEnumerable(IEnumerable<T> elements, IEnumerable<ICompareSelector<T>> sortSelectors)
    {
        _elements = new List<T>(elements);
        _sortSelectors = new LinkedList<ICompareSelector<T>>(sortSelectors);
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
    
    public SortableEnumerable<T> MyThenBy<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>
    {
        _sortSelectors.AddLast(new CompareSelector<T,TKey>(selector));
        return new SortableEnumerable<T>(_elements, _sortSelectors);
    }

    public SortableEnumerable<T> MyOrderBy<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>
    {
        _sortSelectors.AddFirst(new CompareSelector<T, TKey>(selector));
        return new SortableEnumerable<T>(_elements, _sortSelectors);
    }

    private List<T> Materialize()
    {
        var list = _elements.ToList();
        
        list.Sort((a, b) =>
        {
            foreach (var selector in _sortSelectors)
            {
                var comparisonResult = selector.Compare(a, b);
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