using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.util;
using SteeringCS.world;

namespace SteeringCS.entity
{
    public class Dirt : BaseGameEntity
    {
        public Color Color;
        public Dirt(Vector2D pos, World w) : base(pos, w)
        {
            Scale = 10;
            Color = Color.FromArgb(70, 58, 39);
        }

        public override void Update(float delta)
        {
            

        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;
            Brush b = new SolidBrush(Color);
            g.FillEllipse(b, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
        }
    }
}
