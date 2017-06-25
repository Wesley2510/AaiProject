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

            Water = new Water(new Vector2D(700, 450), this);
            WorldGrid.Add(Water);

            var apple1 = new Food(new Vector2D(520, 150), this) { Radius = 15 };
            Food.Add(apple1);
            WorldGrid.Add(apple1);
            var apple2 = new Food(new Vector2D(200, 320), this) { Radius = 15 };
            Food.Add(apple2);
            WorldGrid.Add(apple2);
            var apple3 = new Food(new Vector2D(450, 400), this) { Radius = 15 };
            Food.Add(apple3);
            WorldGrid.Add(apple3);
            var apple4 = new Food(new Vector2D(680, 200), this) { Radius = 15 };
            Food.Add(apple4);
            WorldGrid.Add(apple4);

            var obstacle1 = new Obstacle(new Vector2D(400, 200), this) { Scale = 50 };
            Obstacles.Add(obstacle1);
            WorldGrid.Add(obstacle1);
            var obstacle2 = new Obstacle(new Vector2D(320, 70), this) { Scale = 50 };
            Obstacles.Add(obstacle2);
            WorldGrid.Add(obstacle2);
            var obstacle3 = new Obstacle(new Vector2D(50, 300), this) { Scale = 50 };
            Obstacles.Add(obstacle3);
            WorldGrid.Add(obstacle3);
            var obstacle4 = new Obstacle(new Vector2D(500, 412), this) { Scale = 50 };
            Obstacles.Add(obstacle4);
            WorldGrid.Add(obstacle4);
            var obstacle5 = new Obstacle(new Vector2D(215, 165), this) { Scale = 50 };
            Obstacles.Add(obstacle5);
            WorldGrid.Add(obstacle5);
            var obstacle6 = new Obstacle(new Vector2D(351, 322), this) { Scale = 50 };
            Obstacles.Add(obstacle6);
            WorldGrid.Add(obstacle6);
            var obstacle7 = new Obstacle(new Vector2D(652, 100), this) { Scale = 50 };
            Obstacles.Add(obstacle7);
            WorldGrid.Add(obstacle7);
            var obstacle8 = new Obstacle(new Vector2D(162, 413), this) { Scale = 50 };
            Obstacles.Add(obstacle8);
            WorldGrid.Add(obstacle8);
            var obstacle9 = new Obstacle(new Vector2D(600, 250), this) { Scale = 50 };
            Obstacles.Add(obstacle9);
            WorldGrid.Add(obstacle9);
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
            Obstacles.ForEach(o => o.Render(g));
            Food.ForEach(f => f.Render(g));
            Anthill.Render(g);
            Water.Render(g);
            if (GraphVisible)
            {
                Graph.Render(g);
            }
            Entities.ForEach(e => e.Render(g));
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