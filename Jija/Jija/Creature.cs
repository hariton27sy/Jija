using System.Drawing;
using System.Dynamic;

namespace Jija
{
    public class Creature
    {
        public int Health { get; private set; }
        public PointF Position { get; private set; }
        public int Direction;

        private SizeF velocity;

        public void Update(int dtime)
        {
            if (Position.X > 0)
            {
                Direction = 1;
            }
            else if (Position.Y < 0)
            {
                Direction = -1;
            }

            Position += new SizeF(velocity.Width * dtime / 1000, velocity.Height * dtime / 1000);
        }

        public Creature(Point position)
        {
            Position = position;
            Health = 100;
        }

        public Creature(Point position, int health)
        {
            Position = position;
            Health = health;
        }

        public Add_Velocity(Size adding)
        {
            velocity += adding;
        }
    }
}