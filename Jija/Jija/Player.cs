using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Jija
{
    internal class Player : GameObject
    {
        private const int MaxSpeed = 32;
        private const int JumpingVelocity = 48;
        public int Health { get; private set; }
        public Point LastCheckpoint { get; set; }


        public Player(Point startPosition, int health) : base(startPosition)
        {
            Health = health;
            LastCheckpoint = startPosition;
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
            if (!isJumped)
            {
                Velocity = new Size(Velocity.Width, JumpingVelocity);
                isJumped = true;
            }
        }

        public void ActOnCollision()
        {
            var collisionObject = GetCollisionObject();
            if (collisionObject is Wall)
            {
                Velocity = Size.Empty;
            }
            else if (collisionObject is Enemy)
            {
                Die();
            }
        }

        private void Die()
        {
            Health--;
            Position = LastCheckpoint;
        }
    }
}
