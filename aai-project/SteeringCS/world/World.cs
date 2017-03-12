using System.Collections.Generic;
using System.Drawing;
using SteeringCS.behaviour;
using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.world
{
    public class World
    {
        private List<MovingEntity> _entities = new List<MovingEntity>();
        public Vehicle Target { get; set; }
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
            Vehicle v = new Vehicle(new Vector2D(200, 150), this);
            v.VColor = Color.Blue;
            _entities.Add(v);

            Target = new Vehicle(new Vector2D(100, 60), this);
            Target.VColor = Color.DarkRed;
            Target.Pos = new Vector2D(200, 200);
        }

        public void Update(float timeElapsed)
        {
            foreach (MovingEntity me in _entities)
            {
//                me.Steeringbehaviour = new FleeBehavior(me, Target.Pos);
                me.Steeringbehaviour = new SeekBehaviour(me, Target.Pos);

                me.Update(timeElapsed);
            }
        }

        public void Render(Graphics g)
        {
            _entities.ForEach(e => e.Render(g));
            Target.Render(g);
        }
    }
}