using System.Drawing;
using SteeringCS.util;
using SteeringCS.world;

namespace SteeringCS.entity
{
    public enum DrawType { Fill, Draw }
    public class Vehicle : Ant
    {
        public Color VColor { get; set; }
        public DrawType DrawType { get; set; }

        public Vehicle(Vector2D pos, World w) : base(pos, w)
        {
            Velocity = new Vector2D(0, 0);
            Scale = 5;
            DrawType = DrawType.Draw;
            VColor = Color.Black;
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
            g.DrawLine(p, (int) Pos.X, (int) Pos.Y, (int) Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));
        }
    }
}
