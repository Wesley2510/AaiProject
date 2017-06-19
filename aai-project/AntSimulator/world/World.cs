using System;
using AntSimulator.behaviour;
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
        public Ant Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        private Graph graph;
    

        public World(int w, int h)
        {
            graph = new Graph(this);
            Width = w;
            Height = h;
           graph.FloodFill(graph.startnode);
           
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
            graph.Render(g);
        }

       
        public List<Obstacle> getNearbyObstacles(double length, Vector2D movingEntityPos)
        {
            List<BaseGameEntity> possibleObstacles = WorldGrid.FindNeightbours(movingEntityPos, length);
            List<Obstacle> InterestingObstacles = new List<Obstacle>();
            foreach (BaseGameEntity possibleobstacle in possibleObstacles)
            {
                if (!(possibleobstacle is Obstacle)) continue;
                if (Vector2D.Distance(possibleobstacle.Pos, movingEntityPos) < length)
                    InterestingObstacles.Add((Obstacle)possibleobstacle);
            }
            return InterestingObstacles;
        }
        #region MyRegion




        public List<BaseGameEntity> FindNeightbours(Vector2D pos, double SearchBlock)
        {
            int gridHeight = 15;
            int gridWidth = 15;
            List<BaseGameEntity> neighbours = new List<BaseGameEntity>();
            int cellsDown =
                (int)Math.Ceiling((pos.Y + SearchBlock) / gridHeight - pos.Y / gridHeight);
            int cellsUp =
                (int)Math.Floor((pos.Y - SearchBlock) / gridHeight - pos.Y / gridHeight);
            int cellsLeft =
                (int)Math.Floor((pos.X - SearchBlock) / gridWidth - pos.X / gridWidth);
            int cellsRight =
                (int)Math.Ceiling((pos.X + SearchBlock) / gridWidth - pos.X / gridWidth);
            int collumn = ToCollumn(pos.X);
            int row = ToRow(pos.Y);
            for (var j = cellsUp + collumn; j <= cellsDown + collumn; j++)
            {
                if (j < 0 || j >= GameCollumns) continue;
                for (var i = cellsLeft + row; i <= cellsRight + row; i++)
                {
                    if (i < 0 || i >= GameRows) continue;
                    neighbours.AddRange(grid[j][i]);
                }
            }
            return neighbours;
        }
        public int ToCollumn(double position)
        {
            int result = (int)Math.Floor(position / gridWidth);
            result = result >= GameCollumns ? GameCollumns - 1 : result;
            result = result < 0 ? 0 : result;
            return result;
        }

        public int ToRow(double position)
        {
            int result = (int)Math.Floor(position / gridHeight);
            result = result >= GameRows ? GameRows - 1 : result;
            result = result < 0 ? 0 : result;
            return result;
        }
        #endregion
    }
}