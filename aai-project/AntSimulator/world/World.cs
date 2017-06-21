
using System;
using AntSimulator.behaviour;
using AntSimulator.entity;

using AntSimulator.entity;

using AntSimulator.util;
using System.Collections.Generic;
using System.Drawing;
using AntSimulator.GraphPath;


namespace AntSimulator.world
{
    public class World
    {
        public List<MovingEntity> Entities = new List<MovingEntity>();
        public List<Obstacle> Obstacles = new List<Obstacle>();
        public WorldGrid WorldGrid { get; set; }
        public Obstacle Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool graphVisible;
        private Graph _graph;


        public World(int w, int h)
        {
            Width = w;
            Height = h;

           
            graphVisible = false;
        }

        public void Initialize()
        {
            WorldGrid = new WorldGrid(10, 10, Width, Height);
            Populate();
            _graph = new Graph(this);
            _graph.generateGraph(_graph.startigNode);
        }
        private void Populate()
        {
            Target = new Obstacle(new Vector2D(400, 155), this) { color = Color.DarkRed, size = 5 };
            var ant = new Ant(new Vector2D(10, 10), this) { VColor = Color.Blue };
            Entities.Add(ant);
            WorldGrid.Add(ant);
            WorldGrid.Add(Target);

            var obstacle1 = new Obstacle(new Vector2D(320, 200), this) { color = Color.Black, size = 50 };
            var obstacle2 = new Obstacle(new Vector2D(220, 70), this) { color = Color.Black, size = 100 };
            var obstacle3 = new Obstacle(new Vector2D(50, 250), this) { color = Color.Black, size = 80 };

            Obstacles.Add(obstacle1);
            Obstacles.Add(obstacle2);
            Obstacles.Add(obstacle3);

            WorldGrid.Add(obstacle1);
            WorldGrid.Add(obstacle2);
            WorldGrid.Add(obstacle3);
        }

        public void Update(float timeElapsed)
        {
            foreach (MovingEntity me in Entities)
            {
                me.Update(timeElapsed);
            }
        }

        public void Render(Graphics g)
        {
            Entities.ForEach(e => e.Render(g));
            Obstacles.ForEach(o => o.Render(g));
            Target.Render(g);
            if (graphVisible)
            {
                _graph.Render(g);
            }

        }


        public List<Obstacle> GetNearbyObstacles(double size, Vector2D position)
        {
            List<BaseGameEntity> possibleObstacles = WorldGrid.FindNeighbours(position, size);
            List<Obstacle> interestingObstacles = new List<Obstacle>();
            foreach (var possibleObstacle in possibleObstacles)
            {
                if (!(possibleObstacle is Obstacle)) continue;
                if (Vector2D.Distance(possibleObstacle.Pos, position) < size)
                    interestingObstacles.Add((Obstacle)possibleObstacle);
            }
            return interestingObstacles;
        }

    }
}