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
        private Dictionary<string, Bitmap> img = new Dictionary<string, Bitmap>();
        private readonly Font font = new Font("Courier New", 12);
        private readonly SolidBrush brush = new SolidBrush(Color.FromArgb(242, 255, 0));
        private int CurrentLevel = 1;
        private int windowWidthInBlocks = 10;

        public GameForm()
        {
            var timer = new Timer();
            timer.Interval = 30;
            
            img["player"] = new Bitmap("../../img/player.png");
            img["background"] = new Bitmap("../../img/back.jpg");
            img["wall"] = new Bitmap("../../img/wall.png");
            img["end"] = new Bitmap("../../img/end.png");
            img["sponge"] = new Bitmap("../../img/sponge.png");
            img["bullet"] = new Bitmap("../../img/bullet.png");
            img["hardener"] = new Bitmap("../../img/hardener.png");
            img["lattice"] = new Bitmap("../../img/lattice.png");
            img["heart"] = new Bitmap("../../img/hearth.png");
            img["ammunition"] = new Bitmap("../../img/ammunition.png");
            Console.WriteLine(img["wall"].Height);

            game = new Game("../../Maps/3.txt");
            DoubleBuffered = true;

            img["background"].SetResolution(20, 50);

            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;


            Console.WriteLine(game.Player.Position);

            timer.Tick += (sender, args) =>
            {
                if (game.Player.IsEnd)
                {
                    try
                    {
                        game = new Game($"../../Maps/{CurrentLevel++}.txt");
                        game.Player.IsEnd = false;
                        ClientSize = new Size(windowWidthInBlocks * 32, game.GameSize.Height);
                    }
                    catch
                    {
                        Close();
                    }
                }
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
                case Keys.ControlKey:
                    game.Player.Shoot();
                    Console.WriteLine("HAha");
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
            e.Graphics.TranslateTransform(-game.Player.Position.X + windowWidthInBlocks * 16, 0);
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
                    case Sponge _:
                        e.Graphics.DrawImage(img["sponge"], o.Position);
                        break;
                    case Bullet _:
                        e.Graphics.DrawImage(img["bullet"], o.Position);
                        break;
                    case Hardener _:
                        e.Graphics.DrawImage(img["hardener"], o.Position);
                        break;
                    case Lattice _:
                        e.Graphics.DrawImage(img["lattice"], o.Position);
                        break;
                    case AdditingLife _:
                        e.Graphics.DrawImage(img["heart"], o.Position);
                        break;
                    case AddingAmmunition _:
                        e.Graphics.DrawImage(img["ammunition"], o.Position);
                        break;
                }
            }
            var status = game.Player.IsLiquid ? "Liquid" : "Solid";
            e.Graphics.DrawString($"Lives: {game.Player.Health}\nAmmunition: {game.Player.Ammunition}\nPlayer Status: {status}",
                font, brush, new PointF(game.Player.Position.X - windowWidthInBlocks * 16 , 0));
        }
    }
}
