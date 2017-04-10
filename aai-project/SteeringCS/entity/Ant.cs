using System.Drawing;
using SteeringCS.goals;
using SteeringCS.util;
using SteeringCS.world;

namespace SteeringCS.entity
{
    public enum DrawType { Fill, Draw }
    public class Ant : MovingEntity
    {
        public Color VColor { get; set; }
        public DrawType DrawType { get; set; }
        public ThinkGoal Brain { get; set; }

        public Ant(Vector2D pos, World w) : base(pos, w)
        {
            Velocity = new Vector2D(0, 0);
            Mass = 50;
            MaxSpeed = 10;
            Scale = 5;
            DrawType = DrawType.Draw;
            VColor = Color.Black;
            Brain = new ThinkGoal(this);
        }
        
        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;
           
            Pen p = new Pen(VColor, 2);
            Brush b = new SolidBrush(VColor);
            if (DrawType == DrawType.Draw)
            {
                g.DrawEllipse(p, new Rectangle((int) leftCorner, (int) rightCorner, (int) size, (int) size));
            }
            if (DrawType == DrawType.Fill)
            {
                g.FillEllipse(b, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
            }
        }

        public bool IsAtPosition(Vector2D target)
        {
            const double tolerance = 10.0;
            return Vector2D.DistanceSquared(Pos, target) < tolerance * tolerance;
        }
    }
}
