using AntSimulator.util;
using AntSimulator.world;
using System.Drawing;

namespace AntSimulator.entity
{
    public class Obstacle : BaseGameEntity
    {
        public Obstacle(Vector2D pos, World w) : base(pos, w)
        {
            Radius = 35;
        }

        public override void Update(float delta)
        {
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Radius;
            double rightCorner = Pos.Y - Radius;

            g.DrawImage(Properties.Resources.rock, (int)leftCorner, (int)rightCorner, Radius * 2, Radius * 2);
        }
    }
}