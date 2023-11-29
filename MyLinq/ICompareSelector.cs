namespace MyLinq;

public interface ICompareSelector<T>
{
    int Compare(T a, T b);
}