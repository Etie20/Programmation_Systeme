namespace Tests;

[TestFixture]
public class GroupTest
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void TestGpOrderMeal()
    {
        int a = 1;
        int b = 4;
        int expectedResult = 5;

        int actualResult = a + b;

        Assert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void TestGpOrderMethod()
    {
        
    }
    
    [Test]
    public void GroupMethod()
    {
        
    }
}