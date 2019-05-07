using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jija
{
    class Level
    {
        private char[] enenmiesChars = new[] {'s'};
        public char[,] Map { get; private set; }

        public readonly Player Player;

        public List<IEnemy> Enemies;

        public Level(string filePath, Player player)
        {
            Load_Map(filePath);
            Player = player;
        }

        private void Load_Map(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            Map = new char[lines[0].Length, lines.Length];
            for (var x = 0; x < Map.GetLength(0); x++)
            {
                for (var y = 0; y < lines.Length; y++)
                {
                    Map[x, y] = lines[y][x];
                    if (enenmiesChars.Contains(Map[x, y]))
                    {
                        Add_Enemy(Map[x,y]);
                    }
                }
            }
        }

        private void Add_Enemy(char type)
        {
            if (type == 's')
            {
                Enemies.Add(new Sponge());
            }
        }

        public void UpdateState(int dtime)
        {
            foreach (var enemy in Enemies)
            {
                enemy.Update(dtime);
            }
        }


    }
}
