using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimulationKitchen.Controller;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class CuisineConfigTest
    {
        [TestMethod]
        public void IP_KITCHEN_HasDefaultValue()
        {
            // Assert
            Assert.AreEqual("127.0.0.1", Configuration.IP_KITCHEN);
        }

        [TestMethod]
        public void PORT_KITCHEN_HasDefaultValue()
        {
            // Assert
            Assert.AreEqual(5000, Configuration.PORT_KITCHEN);
        }

        [TestMethod]
        public void CookersNumber_HasDefaultValue()
        {
            // Assert
            Assert.AreEqual(1, Configuration.CookersNumber);
        }
    }
}