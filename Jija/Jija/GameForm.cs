using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Jija
{
    internal class GameForm : Form
    {
        private Game game;
        private Graphics graphics;
        private Dictionary<string, Bitmap> img = new Dictionary<string, Bitmap>();

        public GameForm()
        {
            DoubleBuffered = true;
            var timer = new Timer();
            timer.Interval = 100;
            
            img["player"] = new Bitmap("../../img/player.png");
            img["background"] = new Bitmap("../../img/back.jpg");
            img["wall"] = new Bitmap("../../img/wall.png");

            game = new Game("../../Maps/1.txt");

            ClientSize = game.GameSize;
            graphics = CreateGraphics();

            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;


            timer.Tick += (sender, args) =>
            {
                game.UpdateState();
                Redraw(graphics);
            };
            timer.Start();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    game.Player.Jump();
                    break;

                case Keys.Left:
                    game.Player.Left();
                    break;
                case Keys.Right:
                    game.Player.Right();
                    break;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                game.Player.Stop();
            }
        }
        
        private void Redraw(Graphics graphics)
        {
            graphics.DrawImage(img["background"], new PointF(0, -90));
            foreach(var e in game.objects)
            {
                switch (e)
                {
                    case Wall _:
                        graphics.DrawImage(img["wall"], e.Position);
                        break;
                    case Player _:
                        graphics.DrawImage(img["player"], e.Position);
                        break;
                }
            }
        }
    }
}
