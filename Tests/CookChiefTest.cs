using MCI_Common.Tools;
using SimulationKitchen.Model;

namespace Tests;

public class CookChiefTest
{
    private CookChief _cookChief;

    [SetUp]
    public void Setup()
    {
        _cookChief = new CookChief(new List<Cooker>(), new Counter());
    }

    [Test]
    public void TestAddToolsToWash()
    {
        var tool = new Tool();
        //_cookChief.AddToolsToWash(new List<Tool> { tool });

        //Assert.AreEqual(1, _cookChief.ToolsToWash.Count);
        //Assert.AreEqual(tool, _cookChief.ToolsToWash[0]);
    }

    [Test]
    public void TestStartWorking()
    {
        // This test is more complex because it involves multithreading.
        // You might need to use some synchronization mechanism to ensure that the washer has started working before you check the result.
    }
}