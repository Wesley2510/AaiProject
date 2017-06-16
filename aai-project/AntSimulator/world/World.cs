using AntSimulator.behaviour;
using AntSimulator.entity;
using AntSimulator.util;
using System.Collections.Generic;
using System.Drawing;

namespace AntSimulator.world
{
    public class World
    {
        public List<MovingEntity> Entities = new List<MovingEntity>();
        public List<Obstacle> Obstacles = new List<Obstacle>();
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
            var ant = new Ant(new Vector2D(10, 10), this) { VColor = Color.Blue };
            Entities.Add(ant);

            Target = new Ant(new Vector2D(100, 60), this) { VColor = Color.DarkRed };
            var obstacle1 = new Obstacle(new Vector2D(320, 200), this) { color = Color.Black, size = 100 };
            var obstacle2 = new Obstacle(new Vector2D(220, 70), this) { color = Color.Black, size = 100 };
            var obstacle3 = new Obstacle(new Vector2D(50, 250), this) { color = Color.Black, size = 100 };
            Obstacles.Add(obstacle1);
            Obstacles.Add(obstacle2);
            Obstacles.Add(obstacle3);
        }

        public void Update(float timeElapsed)
        {
            foreach (MovingEntity me in Entities)
            {
                //me.Steeringbehaviour = new Seek(me, Target.Pos);
                me.Steeringbehaviour = new Arrival(me, Target.Pos, Deceleration.Normal);
                me.Update(timeElapsed);
            }
        }

        public void Render(Graphics g)
        {
            Entities.ForEach(e => e.Render(g));
            Obstacles.ForEach(o => o.Render(g));
            Target.Render(g);
        }

        public List<Obstacle> getNearbyObstacles(double length, Vector2D movingEntityPos)
        {
            throw new System.NotImplementedException();
        }
    }
}