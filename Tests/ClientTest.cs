using Room.Model.Client;

namespace Tests;

public class ClientTest
{
    private Client _client;

    [SetUp]
    public void Setup()
    {
        _client = new Client();
    }

    [Test]
    public void TestOrderMeal()
    {
        // This test is more complex because it involves the OrderMeal method which depends on the OrderMethod property.
        // You might need to mock the OrderMethod property to ensure that the test is not affected by its actual implementation.
    }

    [Test]
    public void TestOrderMethod()
    {
        Assert.IsTrue(_client.OrderMethod is OrderAllOne || _client.OrderMethod is OrderTwoStep);
    }
}