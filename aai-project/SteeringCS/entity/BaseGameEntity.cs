using System.Drawing;
using SteeringCS.util;
using SteeringCS.world;

namespace SteeringCS.entity
{
    public abstract class BaseGameEntity
    {
        public Vector2D Pos { get; set; }
        public float Scale { get; set; }
        public World MyWorld { get; set; }

        public BaseGameEntity(Vector2D pos, World w)
        {
            Pos = pos;
            MyWorld = w;
        }

        public abstract void Update(float delta);

        public virtual void Render(Graphics g)
        {
            Image image = new Bitmap(Image.FromFile("ant.bmp"));
            g.FillEllipse(Brushes.SaddleBrown, new Rectangle((int) Pos.X,(int) Pos.Y, 10, 10));
            g.DrawImage(image, new Point((int)Pos.X, (int)Pos.Y));
        }
        

    }
    


    

    
}
