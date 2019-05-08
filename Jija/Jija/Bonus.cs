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

    class AddingAmmunition : Bonus
    {
        public int Ammunition = 15;
        public AddingAmmunition(Point startPosition) : base(startPosition)
        {
            BonusAction = p => p.Ammunition += Ammunition;
        }
    }

    class End : Bonus {
        public End(Point startPosition) : base(startPosition)
        {
            BonusAction = p => p.IsEnd = true;
        }
    }

    class Hardener : Bonus
    {
        public Hardener(Point startPosition) : base(startPosition)
        {
            BonusAction = p => p.IsLiquid = !p.IsLiquid;
        }
    }
}
