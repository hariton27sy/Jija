using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jija
{
    class GameObject
    {
        public static List<GameObject> ObjectsOnMap { get; set; }
        private static Size Gravity = Size.Empty;

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

        public void Update()
        {
            Velocity += Gravity;
            Position += Velocity;
        }

        public GameObject GetCollisionObject()
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
    }
}
