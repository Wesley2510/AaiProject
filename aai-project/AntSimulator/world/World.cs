using AntSimulator.entity;
using AntSimulator.graph;
using AntSimulator.util;
using System.Collections.Generic;
using System.Drawing;


namespace AntSimulator.world
{
    public class World
    {
        public List<MovingEntity> Entities = new List<MovingEntity>();
        public List<Obstacle> Obstacles = new List<Obstacle>();
        public List<Food> Food = new List<Food>();
        public Stack<Ant> AntsToAdd;
        public AntHill Anthill { get; set; }
        public Water Water { get; set; }
        public WorldGrid WorldGrid { get; set; }
        public SeekPoint Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool GraphVisible;
        public bool Activate;
        public bool ShowGoals;
        public Graph Graph { get; set; }

        public World(int w, int h)
        {
            AntsToAdd = new Stack<Ant>();
            Width = w;
            Height = h;
            ShowGoals = false;
            GraphVisible = false;
        }

        public void Initialize()
        {
            WorldGrid = new WorldGrid(10, 10, Width, Height);
            Populate();
            Graph = new Graph(this);
            Graph.GenerateGraph(Graph.StartingNode);
        }
        private void Populate()
        {
            var ant = new Ant(new Vector2D(80, 10), this) { Scale = 10 };
            Entities.Add(ant);
            WorldGrid.Add(ant);

            Anthill = new AntHill(new Vector2D(60, 60), this);
            WorldGrid.Add(Anthill);

            Water = new Water(new Vector2D(600, 400), this);
            WorldGrid.Add(Water);

            var apple1 = new Food(new Vector2D(300, 20), this);
            Food.Add(apple1);
            WorldGrid.Add(apple1);
            var apple2 = new Food(new Vector2D(500, 320), this);
            Food.Add(apple2);
            WorldGrid.Add(apple2);
            var apple3 = new Food(new Vector2D(600, 475), this);
            Food.Add(apple3);
            WorldGrid.Add(apple3);

            var obstacle1 = new Obstacle(new Vector2D(400, 200), this) { Scale = 50 };
            Obstacles.Add(obstacle1);
            WorldGrid.Add(obstacle1);
            var obstacle2 = new Obstacle(new Vector2D(220, 70), this) { Scale = 50 };
            Obstacles.Add(obstacle2);
            WorldGrid.Add(obstacle2);
            var obstacle3 = new Obstacle(new Vector2D(50, 300), this) { Scale = 50 };
            Obstacles.Add(obstacle3);
            WorldGrid.Add(obstacle3);
        }

        public void Update(float timeElapsed)
        {
            while (AntsToAdd.Count > 0)
            {
                Ant a = AntsToAdd.Pop();
                Entities.Add(a);
                WorldGrid.Add(a);
            }
            foreach (MovingEntity me in Entities)
            {
                me.Update(timeElapsed);
            }

        }

        public void Render(Graphics g)
        {
            Entities.ForEach(e => e.Render(g));
            Obstacles.ForEach(o => o.Render(g));
            Food.ForEach(f => f.Render(g));
            Anthill.Render(g);
            Water.Render(g);
            if (GraphVisible)
            {
                Graph.Render(g);
            }

        }


        public List<Obstacle> GetNearbyObstacles(double size, Vector2D position)
        {
            var possibleObstacles = WorldGrid.FindNeighbours(position, size);
            var interestingObstacles = new List<Obstacle>();
            foreach (var possibleObstacle in possibleObstacles)
            {
                if (!(possibleObstacle is Obstacle)) continue;
                if (Vector2D.Distance(possibleObstacle.Pos, position) < size)
                    interestingObstacles.Add((Obstacle)possibleObstacle);
            }
            return interestingObstacles;
        }

        public List<Food> GetNearbyFood(double size, Vector2D position)
        {
            var possibleFood = WorldGrid.FindNeighbours(position, size);
            var interestingFood = new List<Food>();
            foreach (var possFood in possibleFood)
            {
                if (!(possFood is Food)) continue;
                if (Vector2D.Distance(possFood.Pos, position) < size)
                    interestingFood.Add((Food)possFood);
            }
            return interestingFood;
        }

    }
}