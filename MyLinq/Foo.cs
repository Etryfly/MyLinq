namespace MyLinq;

public class Foo
{
    public string A { get; set; } = null!;

    public string B { get; set; } = null!;

    public long C { get; set; }

    public override string ToString()
    {
        return $"{nameof(A)}={A}, {nameof(B)}={B}, {nameof(C)}={C}";
    }
}