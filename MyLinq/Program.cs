using MyLinq;

var collection = new List<Foo>()
{
    new() { A = "b", B = "b", C = 1 },
    new() { A = "a", B = "a", C = 1 },
    new() { A = "a", B = "a", C = 1 },
    new() { A = "a", B = "b", C = 2 },
    new() { A = "a", B = "b", C = 1 },
    new() { A = "b", B = "a", C = 1 },
};

var sorted = collection.MyOrderBy(o => o.A).MyThenBy(o => o.B).MyThenBy(o => o.C).ToList();
foreach (var element in sorted)
{
    Console.WriteLine(element);
}