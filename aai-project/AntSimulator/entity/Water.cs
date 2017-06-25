using AntSimulator.util;
using AntSimulator.world;
using System.Drawing;

namespace AntSimulator.entity
{
    public class Water : BaseGameEntity
    {
        public Water(Vector2D pos, World w) : base(pos, w)
        {
            Radius = 50;
        }

        public override void Update(float delta)
        {
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Radius;
            double rightCorner = Pos.Y - Radius;
            g.DrawImage(Properties.Resources.lake, (int)leftCorner, (int)rightCorner, Radius * 2, Radius * 2);
        }
    }
}
