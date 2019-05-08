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
        public int Health { get; set; }
        public Point LastCheckpoint { get; set; }
        public int Direction;
        public bool IsSolid;
        public int Ammunition;


        public Player(Point startPosition, int health) : base(startPosition)
        {
            Health = health;
            LastCheckpoint = startPosition;
            Ammunition = 15;
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

        public void Shoot()
        {
            if (Ammunition > 0)
            {
                Ammunition--;
                ObjectsOnMap.Add(new Bullet(Direction > -1 ? Position + ObjectSize : Position, Direction));
            }
        }

        public override void Update()
        {
            base.Update();
            ActOnCollision();
        }

        public void ActOnCollision()
        {
            var collisionObject = GetCollisionObject();
            switch (collisionObject)
            {
                case Wall _:
                    Velocity = Size.Empty;
                    RepairCollision(collisionObject);
                    break;
                case Enemy _:
                    Die();
                    break;
            }
        }

        private void Die()
        {
            Health--;
            Position = LastCheckpoint;
        }
    }
}
