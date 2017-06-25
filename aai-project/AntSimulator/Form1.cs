using AntSimulator.world;
using System.Windows.Forms;

namespace AntSimulator
{
    public partial class Form1 : Form
    {
        private World _world;
        private System.Timers.Timer _timer;
        public const float TimeDelta = 0.8f;

        public Form1()
        {
            InitializeComponent();

            _world = new World(dbPanel1.Width, dbPanel1.Height);
            _world.Initialize();
            _timer = new System.Timers.Timer();
            _timer.Elapsed += Timer_Elapsed;
            _timer.Interval = 20;
            _timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _world.Update(TimeDelta);
            dbPanel1.Invalidate();
        }

        private void dbPanel1_Paint(object sender, PaintEventArgs e)
        {
            _world.Render(e.Graphics);
        }

        private void dbPanel1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.G:
                    _world.GraphVisible = !_world.GraphVisible;
                    break;
                case Keys.F:
                    _world.ShowGoals = !_world.ShowGoals;
                    break;
                case Keys.Escape:
                    Application.Exit();
                    break;

            }
        }
    }
}
