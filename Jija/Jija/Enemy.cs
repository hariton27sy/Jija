using System.Drawing;

namespace Jija
{
    internal class Enemy : GameObject
    {
        public Enemy(Point startPosition) : base(startPosition)
        {
        }

        public void ActOnCollision()
        {
            var collisionObject = GetCollisionObject();
            if (collisionObject is Bonus)
            {
                return;;
            }
        }
    }
}