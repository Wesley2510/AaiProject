using SteeringCS.util;
using SteeringCS.world;
using System;
using System.Windows.Forms;

namespace SteeringCS
{
    public partial class Form1 : Form
    {
        private World _world;
        System.Timers.Timer _timer;

        public const float timeDelta = 0.8f;

        public Form1()
        {
            InitializeComponent();

            _world = new World(w: dbPanel1.Width, h: dbPanel1.Height);

            _timer = new System.Timers.Timer();
            _timer.Elapsed += Timer_Elapsed;
            _timer.Interval = 20;
            _timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _world.Update(timeDelta);
            dbPanel1.Invalidate();
        }

        private void dbPanel1_Paint(object sender, PaintEventArgs e)
        {
            _world.Render(e.Graphics);
        }

        private void dbPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            _world.Target.Pos = new Vector2D(e.X, e.Y);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Application.Exit();
                    break;
                case Keys.K:
                    if (World.ShowNodes)
                    {
                        World.ShowNodes = false;
                    }
                    else
                    {
                        World.ShowNodes = true;
                    }
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
