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
    }
}