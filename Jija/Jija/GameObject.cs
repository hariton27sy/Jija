using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jija
{
    internal class GameObject
    {
        public static readonly SizeF ObjectSize = new Size(Game.ObjectsSize, Game.ObjectsSize);
        public static List<GameObject> ObjectsOnMap { get; set; }
        private static SizeF Gravity = new Size(0, 10);
        protected bool IsJumped;

        public PointF Position { get; set; }
        public SizeF Velocity { get; protected set; } = Size.Empty;

        public GameObject(PointF startPosition)
        {
            Position = startPosition;
        }

        public void DestroyObject()
        {
            //что-то еще возможно
            ObjectsOnMap.Remove(this);
        }

        public virtual void Update(int interval)
        {
            if (IsJumped)
            {
                Velocity += Gravity;
            }
            ActIfNoCollision();
            if ((this is Enemy || this is Player) && !IsJumped)
            {
                CheckEmptyDown();
            }
        }

        private void CheckEmptyDown()
        {
            Position += new Size(0, 1);
            if (GetCollisionObject() != null)
            {
                Position -= new Size(0, 1);
            }

        }

        private void ActIfNoCollision()
        {
            var XVelocity = new SizeF(Velocity.Width, 0);
            var YVelocity = new SizeF(0, Velocity.Height);

            Position += XVelocity;
            var collision = GetCollisionObject();
            if (collision != null)
            {
                Position -= XVelocity;
            }

            Position += YVelocity;
            collision = GetCollisionObject();
            if (collision != null)
            {
                Position -= YVelocity;
                IsJumped = false;
            }

        }

        public virtual GameObject GetCollisionObject()
        {
            var size = new Size(Game.ObjectsSize, Game.ObjectsSize);
            var thisRect = new RectangleF(Position, size);
            foreach (var obj in ObjectsOnMap)
            {
                var otherRect = new RectangleF(obj.Position, size);
                if (thisRect.IntersectsWith(otherRect) && obj != this)
                {
                    return obj;
                }
            }
            return null;
        }

        public void RepairCollision(GameObject wall)
        {
            var leftCover = wall.Position.X - Position.X;
            var rightCover = Position.X + Game.ObjectsSize - wall.Position.X;
            var needingX = Math.Abs(leftCover) < Math.Abs(rightCover) ? leftCover : -rightCover;

            var upCover = wall.Position.Y - Position.Y;
            var downCover = Position.Y + Game.ObjectsSize - wall.Position.Y;
            var needingY = Math.Abs(upCover) < Math.Abs(downCover) ? upCover : -downCover;

            Position += Math.Abs(needingX) < Math.Abs(needingY) || needingY == 0 
                ? new SizeF(needingX, 0) : new SizeF(0, needingY);
            if (needingY < 0)
            {
                IsJumped = false;
            }
        }
    }
}
