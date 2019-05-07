using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jija
{
    class Game
    {
        public Player Player = new Player();
        public char[,] Map { get; private set; }

        public Game(string mapFilename)
        {
            
        }
    }
}
