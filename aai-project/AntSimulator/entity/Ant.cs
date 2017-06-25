using AntSimulator.goal;
using AntSimulator.util;
using AntSimulator.world;
using System.Drawing;

namespace AntSimulator.entity
{
    public class Ant : MovingEntity
    {
        public int WorkLoad { get; set; }
        public int Thirst { get; set; }
        public int FoodLoad { get; set; }
        public bool HasFood { get; set; }

        public Ant(Vector2D pos, World w) : base(pos, w)
        {
            Velocity = new Vector2D(0, 0);
            Heading = Vector2D.Normalize(Velocity);
            Scale = 20;
            MaxSpeed = 5;
            WorkLoad = 0;
            HasFood = false;
            Thirst = 100;
            Brain = new GoalThink(this);
            Brain.Activate();
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            var temp = new PointF((float)Pos.X, (float)Pos.Y - 10);
            if (MyWorld.ShowGoals)
                Brain.Render(g, 0, temp);
            g.DrawImage(Properties.Resources.ant, (float)leftCorner, (float)rightCorner, 20, 15);
        }

        public float GetFoodDesirability()
        {
            return 0;
        }
    }
}
