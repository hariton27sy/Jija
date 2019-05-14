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
            var timer = new Timer();
            timer.Interval = 30;
            
            img["player"] = new Bitmap("../../img/player.png");
            img["background"] = new Bitmap("../../img/back.jpg");
            img["wall"] = new Bitmap("../../img/wall.png");
            img["end"] = new Bitmap("../../img/end.png");
            Console.WriteLine(img["wall"].Height);

            game = new Game("../../Maps/1.txt");

            ClientSize = game.GameSize;
            graphics = CreateGraphics();
            DoubleBuffered = true;

            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;


            Console.WriteLine(game.Player.Position);

            timer.Tick += (sender, args) =>
            {
                game.UpdateState(timer.Interval);
                Invalidate();
            };
            timer.Start();

            Paint += Redraw;
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
        
        private void Redraw(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(img["background"], new PointF(0, -90));
            foreach(var o in game.objects)
            {
                switch (o)
                {
                    case Wall _:
                        e.Graphics.DrawImage(img["wall"], o.Position);
                        break;
                    case Player _:
                        e.Graphics.DrawImage(img["player"], o.Position);
                        break;
                    case End _:
                        e.Graphics.DrawImage(img["end"], o.Position);
                        break;
                }
            }
        }
    }
}
