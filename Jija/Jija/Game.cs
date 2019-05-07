using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jija
{
    class Game
    {
        public static int ObjectsSize = 32;

        public Player Player;
        public List<GameObject> objects = new List<GameObject>();

        public Game(string filePath)
        {
            GameObject.ObjectsOnMap = objects;
            Load_Map(filePath);
        }

        private void Load_Map(string filename)
        {
            var y = 0;
            foreach (var line in File.ReadLines(filename))
            {
                var x = 0;
                foreach (var symbol in line)
                {
                    var position = new Point(x * ObjectsSize, y * ObjectsSize);
                    if (symbol == 'p')
                    {
                        Player = new Player(position);
                    }
                    else if (symbol == 's')
                    {
                        objects.Add(new Sponge(position));
                    }
                    else if (symbol == 'w')
                    {
                        objects.Add(null);
                    }
                    
                    x++;
                }
                
                y++;
            }
        }

        public void UpdateState()
        {
            Player.Update();
            foreach (var obj in objects)
            {
                obj.Update();
            }
        }

    }
}