using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jija
{
    class Player : GameObject
    {
        public int Lifes { get; private set; }
        public Player(Point startPosition, int lifes) : base(startPosition)
        {
            Lifes = lifes;
        }

        public void Die()
        {
            if (Lifes > 1)
            {
                Lifes--;
            }
        }
    }
}
