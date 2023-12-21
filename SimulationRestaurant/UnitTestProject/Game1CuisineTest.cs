using betaCuisine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class Game1CuisineTest
    {
        [TestMethod]
        public void Constructor_InitializesProperties()
        {
            // Arrange
            IModel model = new MockModel();

            // Act
            Game1 game = new Game1(model);

            // Assert
            Assert.IsTrue(game.Model == model);
            Assert.AreEqual(0, game.posXWasher);
            Assert.AreEqual(0, game.posYWasher);
            Assert.AreEqual(480, game.posXWasher);
            Assert.AreEqual(320, game.posYWasher);
            //Assert.AreEqual(480, game.graphics.PreferredBackBufferWidth);
            //Assert.AreEqual(320, game.graphics.PreferredBackBufferHeight);
        }
    }
}