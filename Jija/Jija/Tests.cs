using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;


namespace Jija
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test_RepairCollision()
        {
            var player = new Player(new Point(20, 20), 3);
            var wall = new Wall(new Point(50, 20));
            player.RepairCollision(wall);
            Assert.AreEqual(new Point(18, 20), player.Position);
        }

        [Test]
        public void Test_PlayerEnemyCollision()
        {
            var player = new Player(new Point(20, 20), 3);
            var sponge = new Sponge(new Point(20, 20));
            GameObject.ObjectsOnMap = new List<GameObject> { player, sponge };
            player.Update(50);
            Assert.AreEqual(2, player.Health);
        }
    }
}