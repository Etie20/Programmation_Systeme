using MCI_Common.Tools;
using SimulationKitchen.Model;

namespace Tests;

public class WasherTest
{
    private Washer _washer;

    [SetUp]
    public void Setup()
    {
        _washer = new Washer();
    }

    [Test]
    public void TestAddToolsToWash()
    {
        var tool = new Tool();
        _washer.AddToolsToWash(new List<Tool> { tool });

        Assert.AreEqual(1, _washer.ToolsToWash.Count);
        Assert.AreEqual(tool, _washer.ToolsToWash[0]);
    }

    [Test]
    public void TestStartWorking()
    {
        // This test is more complex because it involves multithreading.
        // You might need to use some synchronization mechanism to ensure that the washer has started working before you check the result.
    }
}