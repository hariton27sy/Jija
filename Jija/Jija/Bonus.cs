using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jija
{
    class Bonus : GameObject
    {
        public Action<Player> BonusAction;  
        public Bonus(Point startPosition) : base(startPosition)
        {
        }

        public override void ActOnCollision()
        {
            var collisionObject = GetCollisionObject();
            if (collisionObject is Player)
            {
                BonusAction((Player)collisionObject);
            }
        }
    }

    class AdditingLife : Bonus
    {
        public AdditingLife(Point startPosition) : base(startPosition)
        {
            BonusAction = p => p.Health++;
        }
    }
}
