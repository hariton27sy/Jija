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
        public int Health { get; private set; }
        public Point LastCheckpoint { get; set; }

        public Player(Point startPosition, int health) : base(startPosition)
        {
            Health = health;
            LastCheckpoint = startPosition;
        }

        public void Die()
        {
            if (Health > 1)
            {
                Health--;
                Restart();
            }
            else
            {
                StopGame();
            }
        }

        private void StopGame()
        {
            throw new NotImplementedException();
        }

        private void Restart()
        {
            Position = LastCheckpoint;
        }
    }
}
