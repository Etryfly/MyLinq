using Xunit;

namespace MyLinq.UnitTests;

public class MyLinqTests
{
    [Fact]
    public void OrderByThenByOrderBy_ResultEqualsToMicrosoft()
    {
        var collection = GenerateListOfRandomFoo(100);

        var mySorted = collection.MyOrderBy(o => o.A).MyThenBy(o => o.B).MyOrderBy(o => o.C).ToList();
        var msSorted = collection.OrderBy(o => o.A).ThenBy(o => o.B).OrderBy(o => o.C).ToList();
        
        Assert.Equal(msSorted, mySorted);
    }
    
    [Fact]
    public void OrderByThenByThenBy_ResultEqualsToMicrosoft()
    {
        var collection = GenerateListOfRandomFoo(100);

        var mySorted = collection.MyOrderBy(o => o.A).MyThenBy(o => o.B).MyThenBy(o => o.C).ToList();
        var msSorted = collection.OrderBy(o => o.A).ThenBy(o => o.B).ThenBy(o => o.C).ToList();
        
        Assert.Equal(msSorted, mySorted);
    }
    
    
    [Fact]
    public void OrderByOrderByOrderBy_ResultEqualsToMicrosoft()
    {
        var collection = GenerateListOfRandomFoo(100);

        var mySorted = collection.MyOrderBy(o => o.A).MyOrderBy(o => o.B).MyOrderBy(o => o.C).ToList();
        var msSorted = collection.OrderBy(o => o.A).OrderBy(o => o.B).OrderBy(o => o.C).ToList();
        
        Assert.Equal(msSorted, mySorted);
    }

    private List<Foo> GenerateListOfRandomFoo(int count)
    {
        return Enumerable.Range(1, count).Select(_ => GenerateRandomFoo()).ToList();
    }
    
    private Foo GenerateRandomFoo()
    {
        var random = new Random();
        return new Foo()
        {
            A = Guid.NewGuid().ToString(),
            B = Guid.NewGuid().ToString(),
            C = random.NextInt64()
        };
    }
}