using AntSimulator.util;
using AntSimulator.world;
using System.Drawing;
using AntSimulator.Goals;

namespace AntSimulator.entity
{
    public class Ant : MovingEntity
    {
        public Ant(Vector2D pos, World w) : base(pos, w)
        {
            Velocity = new Vector2D(0, 0);
            Scale = 20;
            MaxSpeed = 5;
           
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;

            g.DrawImage(Properties.Resources.ant, (float)leftCorner, (float)rightCorner, 20, 15);
            //g.DrawEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)Scale * 2, (int)Scale * 2));
            //g.DrawLine(p, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));
        }
    }
}
