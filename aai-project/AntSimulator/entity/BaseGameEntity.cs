using AntSimulator.util;
using AntSimulator.world;
using System.Drawing;
using AntSimulator.Goals;

namespace AntSimulator.entity
{
    public abstract class BaseGameEntity
    {
        public Vector2D Pos { get; set; }
        public float Scale { get; set; }
        public float Radius { get; set; }
        public Color Color { get; set; }
        public World MyWorld { get; set; }
        public Goal goals { get; set; }

        protected BaseGameEntity(Vector2D pos, World w)
        {
            
            Pos = pos;
            MyWorld = w;
        }

        public abstract void Update(float delta);

        public virtual void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Blue, new Rectangle((int)Pos.X, (int)Pos.Y, 10, 10));
        }
    }
}
