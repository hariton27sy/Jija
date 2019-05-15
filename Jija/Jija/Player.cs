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
        private const int JumpingVelocity = 35;
        public int Health { get; set; }
        public Point LastCheckpoint { get; set; }
        public int Direction;
        public int Ammunition;
        public bool IsEnd = true;
        public bool IsLiquid = true;


        public Player(Point startPosition, int health) : base(startPosition)
        {
            Health = health;
            LastCheckpoint = startPosition;
            Ammunition = 15;
        }

        public void Left()
        {
            Velocity = new SizeF(Math.Max(Velocity.Width - 10, -MaxSpeed), Velocity.Height);
        }

        public void Right()
        {
            Velocity = new SizeF(Math.Min(Velocity.Width + 10, MaxSpeed), Velocity.Height);
        }

        public void Stop()
        {
            Velocity = new SizeF(0, Velocity.Height);
        }

        public void Jump()
        {
            if (!IsJumped)
            {
                Velocity = new SizeF(Velocity.Width, -JumpingVelocity);
                IsJumped = true;
            }
        }

        public void Shoot()
        {
            if (Ammunition > 0)
            {
                Ammunition--;
                ObjectsOnMap.Add(new Bullet(Direction > -1 ? Position + new Size(33, 0) : Position + new Size(0,16), Direction));
            }
        }

        public override void Update(int interval)
        {
            base.Update(interval);
            ActOnCollision();
            if (Position.Y > MapSize.Height)
            {
                Die();
            }
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
                case Sponge _:
                    Die();
                    break;
                case Lattice _:
                    if (!IsLiquid)
                    {
                        Velocity = Size.Empty;
                        RepairCollision(collisionObject);
                    }
                    break;
                case Bonus _:
                    ((Bonus)collisionObject).BonusAction(this);
                    collisionObject.DestroyObject();
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
