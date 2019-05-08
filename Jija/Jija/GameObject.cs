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
        public static readonly Size ObjectSize = new Size(Game.ObjectsSize, Game.ObjectsSize);
        public static List<GameObject> ObjectsOnMap { get; set; }
        private static Size Gravity = Size.Empty;
        protected bool IsJumped;

        public Point Position { get; set; }
        public Size Velocity { get; protected set; } = Size.Empty;

        public GameObject(Point startPosition)
        {
            Position = startPosition;
        }

        public void DestroyObject()
        {
            //что-то еще возможно
            ObjectsOnMap.Remove(this);
        }

        public virtual void Update()
        {
            Velocity += Gravity;
            Position += Velocity;

        }

        public virtual void ActOnCollision()
        {

        }

        public virtual GameObject GetCollisionObject()
        {
            var size = new Size(Game.ObjectsSize, Game.ObjectsSize);
            var thisRect = new Rectangle(Position, size);
            foreach (var obj in ObjectsOnMap)
            {
                var otherRect = new Rectangle(obj.Position, size);
                if (thisRect.IntersectsWith(otherRect))
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
                ? new Size(needingX, 0) : new Size(0, needingY);
            if (needingY < 0)
            {
                IsJumped = false;
            }
        }
    }
}
