using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jija
{
    class Game
    {
        public Player Player;
        public Game(string filePath)
        {
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
                    if (symbol == 'p')
                    {
                        Player = new Player();
                    }
                    
                    x++;
                }
                
                y++;
            }
        }
    }
}
