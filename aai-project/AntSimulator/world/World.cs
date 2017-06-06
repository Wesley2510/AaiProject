using AntSimulator.behaviour;
using AntSimulator.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSimulator
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
            populate();
        }

        private void populate()
        {
            Ant v = new Ant(new Vector2D(10,10), this);
            v.VColor = Color.Blue;
            entities.Add(v);

            Target = new Ant(new Vector2D(100, 60), this);
            Target.VColor = Color.DarkRed;
            Target.Pos = new Vector2D(100, 40);
        }

        public void Update(float timeElapsed)
        {
            foreach (MovingEntity me in entities)
            {
                // me.SB = new SeekBehaviour(me); // restore later
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
