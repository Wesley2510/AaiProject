using AntSimulator.entity;
using AntSimulator.graph;
using AntSimulator.util;
using System.Collections.Generic;
using System.Drawing;
using AntSimulator.goal;


namespace AntSimulator.world
{
    public class World
    {
        public List<MovingEntity> Entities = new List<MovingEntity>();
        public List<Obstacle> Obstacles = new List<Obstacle>();
        public List<Food> Food = new List<Food>();
        public AntHill Anthill { get; set; }
        public WorldGrid WorldGrid { get; set; }
        public SeekPoint Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool GraphVisible;
        public bool Activate;
        public Graph Graph { get; set; }
        // public Ant walkerAnt;


        public World(int w, int h)
        {
            Width = w;
            Height = h;

            GraphVisible = false;
        }

        public void Initialize()
        {
            WorldGrid = new WorldGrid(10, 10, Width, Height);
            Populate();
            Graph = new Graph(this);
            Graph.GenerateGraph(Graph.StartingNode);

            PopulateAnts();

        }
        private void Populate()
        {
            Target = new SeekPoint(new Vector2D(400, 155), this) { Color = Color.GreenYellow, Scale = 5 };

            Anthill = new AntHill(new Vector2D(60, 60), this);

            var apple1 = new Food(new Vector2D(300, 20), this);
            var apple2 = new Food(new Vector2D(500, 320), this);
            var apple3 = new Food(new Vector2D(600, 475), this);

            WorldGrid.Add(Target);

            WorldGrid.Add(Anthill);

            WorldGrid.Add(apple1);
            WorldGrid.Add(apple2);
            WorldGrid.Add(apple3);

            var obstacle1 = new Obstacle(new Vector2D(400, 200), this) { Scale = 50 };
            var obstacle2 = new Obstacle(new Vector2D(220, 70), this) { Scale = 50 };
            var obstacle3 = new Obstacle(new Vector2D(50, 300), this) { Scale = 50 };

            Obstacles.Add(obstacle1);
            Obstacles.Add(obstacle2);
            Obstacles.Add(obstacle3);

            WorldGrid.Add(obstacle1);
            WorldGrid.Add(obstacle2);
            WorldGrid.Add(obstacle3);

            Food.Add(apple1);
            Food.Add(apple2);
            Food.Add(apple3);
        }

        private void PopulateAnts()
        {
            var ant = new Ant(new Vector2D(80, 10), this) { Scale = 10 };
            ant.goals = new GoalSeek(ant, Target.Pos, 10);

            var ant2 = new Ant(new Vector2D(20, 20), this) { Scale = 10 };
            ant2.goals = new GoalArrival(ant2, Target.Pos, 20);

            var ant3 = new Ant(new Vector2D(120, 20), this) { Scale = 10 };
            ant3.goals = new GoalGetFood(ant3);


            var ant4 = new Ant(new Vector2D(20, 20), this) { Scale = 10 };
            ant4.goals = new GoalFollowPath(ant4, Target.Pos);

            //walkerAnt = new Ant(new Vector2D(20, 20), this) { Scale = 10 };
            //walkerAnt.goals = new GoalFollowPath(walkerAnt, Target.Pos);


            //    Entities.Add(ant);
            //  WorldGrid.Add(ant);

            //   Entities.Add(ant2);
            //   WorldGrid.Add(ant2);

            Entities.Add(ant3);
            WorldGrid.Add(ant3);

            // Entities.Add(ant4);
            // WorldGrid.Add(ant4);


        }

        public void Update(float timeElapsed)
        {
            foreach (MovingEntity me in Entities)
            {
                if (Activate)
                {
                    me.goals.Activate();
                }
                me.Update(timeElapsed);
            }
        }

        public void Render(Graphics g)
        {
            Entities.ForEach(e => e.Render(g));
            Obstacles.ForEach(o => o.Render(g));
            Food.ForEach(f => f.Render(g));
            Target.Render(g);
            Anthill.Render(g);
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