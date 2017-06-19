using AntSimulator.entity;
using AntSimulator.util;
using System.Collections.Generic;
using System.Drawing;
using AntSimulator.GraphPath;


namespace AntSimulator.world
{
    public class World
    {
        private List<MovingEntity> entities = new List<MovingEntity>();
        public Ant Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        private Graph graph;
    

        public World(int w, int h)
        {
            graph = new Graph(this);
            Width = w;
            Height = h;
           // graph.FloodFill(graph.startnode);
            populate();
        }

        private void populate()
        {
            Ant v = new Ant(new Vector2D(10, 10), this);
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
                // me.Steeringbehaviour = new SeekBehaviour(me); // restore later
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
