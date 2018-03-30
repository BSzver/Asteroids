using System;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroids
{
    public class BaseObject
    {
        public BaseObject()
        {
            
        }

        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        private BufferedGraphicsContext _context;
        public BufferedGraphics Buffer;
        public int Width { get; set; }
        public int Height { get; set; }

        public void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();

            Timer timer = new Timer { Interval = 50 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        public virtual void Draw()
        {
            Image image = Image.FromFile("asteroid.png");
            Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
                
        public virtual void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y - Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
            if (Pos.X < 0) Pos.X = Game.Width - Size.Width;
        }

        public BaseObject[] _objs;
        public void Load()
        {
            _objs = new BaseObject[30];
            MoveStar();
        }

        public void MoveStar()
        {
            for (int i = 0; i < _objs.Length / 2; i++)
            {
                _objs[i] = new BaseObject(new Point(600, i * 20), new Point(-i, -i), new Size(30, 30));
                _objs[i] = new BaseObject(new Point(600, i * 20), new Point(-i, -i), new Size(15, 15));
                _objs[i] = new Star(new Point(600, i * 20), new Point(-i, 0), new Size(5, 5));
            }
        }
    }
}
