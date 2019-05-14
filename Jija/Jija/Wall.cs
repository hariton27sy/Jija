using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jija
{
    internal class Wall : GameObject
    {
        public Wall(Point position) : base(position)
        {
        }
    }

    internal class Lattice : GameObject
    {
        public Lattice(Point startPosition) : base(startPosition)
        {
        }
    }

    internal class Bullet : GameObject
    {
        public Bullet(PointF startPosition, int direction) : base(startPosition)
        {
            Velocity = new Size(64 * direction, 0);
        }

        public void ActOnCollision()
        {
            var collisionObject = GetCollisionObject();

            if (collisionObject is Enemy)
            {
                collisionObject.DestroyObject();
            }
            DestroyObject();
        }
    }
}
