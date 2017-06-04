using SteeringCS.entity;
using System.Collections.Generic;
using System.Drawing;

namespace SteeringCS
{
    class World
    {
        private List<MovingEntity> entities = new List<MovingEntity>();
        public Ant Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            Populate();
        }

        private void Populate()
        {
            Ant v = new Ant(new Vector2D(10, 10), this)
            {
                VColor = Color.Blue
            };
            entities.Add(v);

            Target = new Ant(new Vector2D(100, 60), this)
            {
                VColor = Color.DarkRed,
                Pos = new Vector2D(100, 40)
            };
        }

        public void Update(float timeElapsed)
        {
            foreach (MovingEntity me in entities)
            {
                me.Update(timeElapsed);
            }  
        }

        public void Render(Graphics g)
        {
            entities.ForEach(e => e.Render(g));
            Target.Render(g);
        }
    }
}
