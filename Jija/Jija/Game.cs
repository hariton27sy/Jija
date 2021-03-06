﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jija
{
    public static class SizeExtansion
    {
        public static SizeF Scale(this SizeF size1, float scale)
        {
            return new SizeF(size1.Width * scale, size1.Height * scale);
        }
    }

    internal class Game
    {
        public const int ObjectsSize = 32;

        public Size GameSize { get; private set; }

        public Player Player;
        public readonly List<GameObject> objects = new List<GameObject>();

        public bool IsGameOver => Player?.Health == 0;

        public Game(string filePath)
        {
            GameObject.ObjectsOnMap = objects;
            Load_Map(filePath);
            GameObject.MapSize = GameSize;
        }

        private void Load_Map(string filename)
        {
            var y = 0;
            var x = 0;
            foreach (var line in File.ReadLines(filename))
            {
                x = 0;
                foreach (var symbol in line)
                {
                    var position = new Point(x * ObjectsSize, y * ObjectsSize);
                    switch (symbol)
                    {
                        case 'h':
                            objects.Add(new Hardener(position));
                            break;
                        case 't':
                            objects.Add(new Lattice(position));
                            break;
                        case 'p':
                            Player = new Player(position, 3);
                            objects.Add(Player);
                            break;
                        case 's':
                            objects.Add(new Sponge(position));
                            break;
                        case 'w':
                            objects.Add(new Wall(position));
                            break;
                        case 'e':
                            objects.Add(new End(position));
                            break;
                        case 'l':
                            objects.Add(new AdditingLife(position));
                            break;
                        case 'a':
                            objects.Add(new AddingAmmunition(position));
                            break;
                        case '.':
                            break;
                        default:
                            //throw new ArgumentException("Unresolved symbol in map declaration");
                            break;
                    }
                    x++;
                }
                y++;
            }

            GameSize = new Size(32 * x, 32 * y);
        }

        public void UpdateState(int interval)
        {
            Player.Update(interval);
            foreach (var obj in objects)
            {
                obj.Update(interval);
            }
            GameObject.UpdateDestroyedObjects();
        }
    }
}