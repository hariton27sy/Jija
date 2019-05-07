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
        public static List<GameObject> ObjectsOnMap;

        public Point Position { get; set; }
        public Size Velocity { get; private set; }

        public GameObject(Point startPosition)
        {
            Position = startPosition;
            Velocity = new Size(32, 0);
        }

        public void DestroyObject()
        {
            //что-то еще возможно
            ObjectsOnMap.Remove(this);
        }

        public void Update()
        {
            
        }
    }
}
