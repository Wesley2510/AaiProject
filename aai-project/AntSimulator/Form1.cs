﻿using AntSimulator.util;
using AntSimulator.world;
using System.Windows.Forms;

namespace AntSimulator
{
    public partial class Form1 : Form
    {
        World world;
        
        System.Timers.Timer timer;

        public const float timeDelta = 0.8f;

        public Form1()
        {
            InitializeComponent();

            world = new World(dbPanel1.Width, dbPanel1.Height);
            world.Initialize();
            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 20;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            world.Update(timeDelta);
            dbPanel1.Invalidate();
        }

        private void dbPanel1_Paint(object sender, PaintEventArgs e)
        {
            world.Render(e.Graphics);
        }

        private void dbPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            world.Target.Pos = new Vector2D(e.X, e.Y);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Z:
                    if (world.graphVisible)
                    {
                        world.graphVisible = false;
                    }
                    else
                    {
                        world.graphVisible = true;
                    }
                    break;
            }
        }
    }
}
