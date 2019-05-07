using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Jija
{
    class Player : GameObject
    {
        public static int MaxSpeed = 32;
        public static int JumpingVelocity = 48;
        public int Health { get; private set; }
        public Point LastCheckpoint { get; set; }
        public bool IsJumped = false;


        public Player(Point startPosition, int health) : base(startPosition)
        {
            Health = health;
            LastCheckpoint = startPosition;
        }

        public void Die()
        {
            Health--;
            Position = LastCheckpoint;
        }

        public void Left()
        {
            Velocity = new Size(Math.Max(Velocity.Width - 2, -MaxSpeed), Velocity.Height);
        }

        public void Right()
        {
            Velocity = new Size(Math.Min(Velocity.Width + 2, MaxSpeed), Velocity.Height);
        }

        public void Stop()
        {
            Velocity = new Size(0, Velocity.Height);
        }

        public void Jump()
        {
            if (!IsJumped)
            {
                Velocity = new Size(Velocity.Width, JumpingVelocity);
                IsJumped = true;
            }
        }
    }
}
