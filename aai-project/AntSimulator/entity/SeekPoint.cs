using AntSimulator.util;
using AntSimulator.world;
using System.Drawing;

namespace AntSimulator.entity
{
    public class SeekPoint : BaseGameEntity
    {
        public SeekPoint(Vector2D pos, World w) : base(pos, w)
        {
        }

        public override void Update(float delta)
        {
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            Pen p = new Pen(Color, 2);

            g.DrawEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)Scale * 2, (int)Scale * 2));
        }
    }
}
