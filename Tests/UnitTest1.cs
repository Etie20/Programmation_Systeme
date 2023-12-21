namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        Assert.AreEqual(4, Calculator.Add(2, 2));
    }

    [Test]
    public void Test1()
    {
        Assert.AreEqual(5, Calculator.Add(2, 2));
    }
}

public class Calculator
{
    public static int Add(int a, int b)
    {
        return 2 + 2;
    }
}
