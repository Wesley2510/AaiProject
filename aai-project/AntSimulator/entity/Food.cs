using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntSimulator.util;
using AntSimulator.world;

namespace AntSimulator.entity
{
    public class Food :BaseGameEntity
    {
        public Food(Vector2D pos, World w) : base(pos, w)
        {
            Radius = 10;
        }

        public override void Update(float delta)
        {
            throw new NotImplementedException();
        }
        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Radius;
            double rightCorner = Pos.Y - Radius;

            g.DrawImage(Properties.Resources.apple, (int)leftCorner, (int)rightCorner, Radius * 2, Radius * 2);
        }
    }
}
