﻿using System;
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
        public Size Velocity { get; private set; } = Size.Empty;

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
    }
}
