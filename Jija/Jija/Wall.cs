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

    internal class Bullet : GameObject
    {
        public Bullet(Point startPosition) : base(startPosition)
        {
            Velocity = new Size(64, 0);
        }

        public override void ActOnCollision()
        {
            base.ActOnCollision();
            var collisionObject = GetCollisionObject();

            if (collisionObject is Enemy)
            {
                collisionObject.DestroyObject();
            }
            DestroyObject();
        }
    }
}
