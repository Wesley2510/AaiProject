using AntSimulator.util;
using AntSimulator.world;
using System.Drawing;

namespace AntSimulator.entity
{
    public class Food : BaseGameEntity
    {
        public int FoodPercentage { get; set; }
        public Food(Vector2D pos, World w) : base(pos, w)
        {
            FoodPercentage = 100;
            Radius = 10;
        }

        public override void Update(float delta)
        {
        }
        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Radius;
            double rightCorner = Pos.Y - Radius;

            g.DrawImage(Properties.Resources.apple, (int)leftCorner, (int)rightCorner, Radius * 2, Radius * 2);
        }
    }
}
