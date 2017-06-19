using AntSimulator.util;
using AntSimulator.world;
using System;
using System.Drawing;

namespace AntSimulator.entity
{
    public class Obstacle : BaseGameEntity
    {
        public Color color;
        public float size = 20;

        public Obstacle(Vector2D pos, World w) : base(pos, w)
        {

        }

        public override void Update(float delta)
        {
            throw new NotImplementedException();
        }

        public override void Render(Graphics g)
        {
            Brush p = new SolidBrush(color);
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;

            g.FillEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
        }
    }
}
