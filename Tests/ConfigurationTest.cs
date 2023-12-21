using NUnit.Framework;
using SimulationKitchen.Controller;

namespace Tests;

public class ConfigurationTest
{
    [Test]
    public void TestIP_KITCHEN()
    {
        Assert.AreEqual("127.0.0.1", Configuration.IP_KITCHEN);
    }

    [Test]
    public void TestPORT_KITCHEN()
    {
        Assert.AreEqual(5000, Configuration.PORT_KITCHEN);
    }

    [Test]
    public void TestCookersNumber()
    {
        Assert.AreEqual(1, Configuration.CookersNumber);
    }
}