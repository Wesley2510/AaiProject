using AntSimulator.util;
using AntSimulator.world;
using System.Drawing;
using AntSimulator.goal;

namespace AntSimulator.entity
{
    public class Ant : MovingEntity
    {
        public int WorkLoad { get; set; }
        public int Thirst { get; set; }
        public bool HasFood { get; set; }
        public Ant(Vector2D pos, World w) : base(pos, w)
        {
            Velocity = new Vector2D(0, 0);
            Scale = 20;
            MaxSpeed = 5;
            WorkLoad = 0;
            HasFood = false;
            Thirst = 0;
            Brain = new AntThinkGoal(this);
            Brain.Activate();
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;

            g.DrawImage(Properties.Resources.ant, (float)leftCorner, (float)rightCorner, 20, 15);
        }
    }
}
