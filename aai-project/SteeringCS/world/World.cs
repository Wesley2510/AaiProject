using System;
//using System.Drawing;
//using SteeringCS.behaviour;
//using SteeringCS.entity;
//using SteeringCS.State;
//using SteeringCS.util;

//namespace SteeringCS.world
//{
//    public class World
//    {
//        private List<MovingEntity> _entities = new List<MovingEntity>();
//        public Ant Target { get; set; }
//        public int Width { get; set; }
//        public int Height { get; set; }

//        public World(int w, int h)
//        {
//            Width = w;
//            Height = h;
//            Populate();
//        }

//        private void Populate()
//        {
//            Ant v = new Ant(new Vector2D(200, 150), this) {VColor = Color.Blue};
//            //v.ChangeState(new State_LookForFood());
//            _entities.Add(v);

//            Target = new Ant(new Vector2D(100, 60), this)
//            {
//                VColor = Color.DarkRed,
//                Pos = new Vector2D(400, 400)
//            };
//        }

//        public void Update(float timeElapsed)
//        {
//            foreach (MovingEntity me in _entities)
//            {

//                if (me.currState != null)
//                {
//                    me.currState.Execute(me);
//                }
//                else
//                {
//                    me.ChangeState(new State_LookForFood());
//                }

//                //me.Steeringbehaviour = new FleeBehavior(me, Target.Pos);
//                //me.Steeringbehaviour = new ArrivalBehavior(me, Target.Pos, Deceleration.Slow);
//                //me.Steeringbehaviour = new SeekBehaviour(me, Target.Pos);
//              //  me.Steeringbehaviour = new WanderBehaviour(me);
//                me.Update(timeElapsed);
//            }
//        }

//        public void Render(Graphics g)
//        {
//            _entities.ForEach(e => e.Render(g));
//            Target.Render(g);
//        }
//    }
//}
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Policy;
using SteeringCS.behaviour;
using SteeringCS.entity;
using SteeringCS.graphs;
using SteeringCS.States;
using SteeringCS.util;

namespace SteeringCS.world
{
    public class World
    {
        public static bool showNodes = true;
        private List<MovingEntity> _entities = new List<MovingEntity>();
        private List<Dirt> _objects = new List<Dirt>();
        private List<Node> Nodes = new List<Node>();
        public Ant Target { get; set; }
        public static Ant Enemy { get; set; }
        public Graph Graph = new Graph();
        public int Width { get; set; }
        public int Height { get; set; }
        private PathPlanner _pathPlanner;
        public static Node food;
        public World(int w, int h)
        {
            food = new Node(25, new Vector2D(600, 50));
            food.ExtraInfo = ExtraInfo.HasFood;
            Width = w;
            Height = h;
            Populate();
            BuildGraph(30, 150, 43, 65);
            /*Graph.Astar(9, 25);
            Graph.PrintPath(25);*/
        }

        private void Populate()
        {
            var a1 = new Ant(new Vector2D(50, 225), this) { VColor = Color.Black, DrawType = DrawType.Fill };
            _entities.Add(a1);
            var d1 = new Dirt(new Vector2D(600, 300), this);
            _objects.Add(d1);
            var d2 = new Dirt(new Vector2D(200, 500), this);
            _objects.Add(d2);
            var d3 = new Dirt(new Vector2D(300, 300), this);
            _objects.Add(d3);
            var d4 = new Dirt(new Vector2D(580, 520), this);
            _objects.Add(d4);
            Target = new Ant(new Vector2D(380, 380), this)
            {
                VColor = Color.White
            };

        }

        private void BuildGraph(int startx, int starty, int numrows, int numcolums)
        {
            var tempx = startx;
            var tempy = starty;
            var count = 0;
            Node TempNode1 = null;
            Node TempNode2 = null;
            //var count = 0;
            //for (int i = 0; i < numrows; i++)
            //{
            //    Graph.AddEdge(i, new Vector2D(tempx, starty), i + 1, new Vector2D(tempx + 10, starty), 1);
            //    tempx += 10;
            //    count++;
            //}
            //int j = count;
            //for (j = 0; j < numcolums; j++)
            //{
            //    Graph.AddEdge(j, new Vector2D(startx, tempy), j + 1, new Vector2D(startx, tempy+ 10), 1);
            //    tempy += 10;
            //}
            for (int i = 0; i < numrows; i++)
            {
                for (int k = 0; k < numcolums; k++)
                {

                    Node node = new Node(new Vector2D(tempx + (k * 10), tempy + (i * 10)));

                    if (checkNode(node))
                    {
                        Nodes.Add(node);
                    }
                    else
                    {
                        Console.Write(" FAILED!");
                    }

                }
                int counter = 0;
                foreach (Node node in Nodes)
                {

                    if (TempNode1 == null)
                    {
                        TempNode1 = node;
                    }
                    else
                    {
                        TempNode2 = TempNode1;
                        TempNode1 = node;

                        if (counter % numrows != 0)
                        {
                            Graph.AddEdge(TempNode2.Index, TempNode2.Postition, TempNode1.Index, TempNode1.Postition, 1);
                        }
                    }
                    counter++;
                }
            }
        }

        public bool checkNode(Node node)
        {
            double x = node.Postition.X;
            double y = node.Postition.Y;
            //_objects = linkedWorld.getNearbyObstacles(maxRadius + 5, node.position);
            if (x < 0 || y < 0 || x > 800 || y > 600 || Graph.NodeMap.ContainsKey(node.Index))
            {
                return false;
            }
            foreach (Dirt dirt in _objects)
            {
                if (Vector2D.GetDistanceBetweenVectors(node.Postition, dirt.Pos) < dirt.Scale)
                // +1 to be absolutely sure that nothing weird happens.
                {
                    return false;
                }
            }
            return true;
        }



        /*Graph.AddEdge(1, new Vector2D(50, 225), 2, new Vector2D(100, 260), 1);
        Graph.AddEdge(2, new Vector2D(100, 260), 3, new Vector2D(60, 320), 1);
        Graph.AddEdge(3, new Vector2D(60, 320), 4, new Vector2D(60, 390), 1);
        Graph.AddEdge(4, new Vector2D(60, 390), 5, new Vector2D(125, 430), 1);
        Graph.AddEdge(5, new Vector2D(125, 380), 6, new Vector2D(215, 380), 1);
        Graph.AddEdge(6, new Vector2D(215, 380), 7, new Vector2D(300, 325), 1);
        Graph.AddEdge(7, new Vector2D(300, 325), 8, new Vector2D(385, 285), 1);
        Graph.AddEdge(8, new Vector2D(385, 285), 9, new Vector2D(460, 240), 1);
        Graph.AddEdge(9, new Vector2D(460, 240), 10, new Vector2D(545, 210), 1);
        Graph.AddEdge(10, new Vector2D(545, 210), 11, new Vector2D(430, 190), 1);
        Graph.AddEdge(11, new Vector2D(430, 190), 8, new Vector2D(385, 285), 1);
        Graph.AddEdge(12, new Vector2D(430, 190), 9, new Vector2D(460, 240), 1);
        Graph.AddEdge(10, new Vector2D(545, 210), 14, new Vector2D(635, 200), 1);
        Graph.AddEdge(14, new Vector2D(635, 200), 15, new Vector2D(640, 120), 1);
        Graph.AddEdge(15, new Vector2D(640, 120), 10, new Vector2D(545, 210), 1);
        Graph.AddEdge(14, new Vector2D(635, 200), 17, new Vector2D(620, 315), 1);
        Graph.AddEdge(17, new Vector2D(620, 315), 18, new Vector2D(610, 415), 1);
        Graph.AddEdge(18, new Vector2D(610, 415), 19, new Vector2D(580, 500), 1);
        Graph.AddEdge(19, new Vector2D(590, 500), 20, new Vector2D(510, 585), 1);
        Graph.AddEdge(17, new Vector2D(620, 315), 21, new Vector2D(530, 370), 1);
        Graph.AddEdge(21, new Vector2D(530, 370), 22, new Vector2D(435, 400), 1);
        Graph.AddEdge(22, new Vector2D(435, 400), 23, new Vector2D(320, 450), 1);
        Graph.AddEdge(23, new Vector2D(320, 450), 24, new Vector2D(245, 520), 1);
        Graph.AddEdge(24, new Vector2D(245, 520), 25, new Vector2D(200, 580), 1);*/
        //Graph.AddEdge(0, new Vector2D(0, 0), 0, new Vector2D(0, 0), 1);


        public void Update(float timeElapsed)
        {
            foreach (var me in _entities)
            {
                _pathPlanner = new PathPlanner(me, this);
                //me.Steeringbehaviour = new FleeBehavior(me, Target.Pos);
                me.Steeringbehaviour = new ArrivalBehavior(me, Target.Pos, Deceleration.Slow);
                //me.Steeringbehaviour = new SeekBehaviour(me, Target.Pos);
                //me.Steeringbehaviour = new WanderBehaviour(me);
                me.Update(timeElapsed);
            }
        }

        public void Render(Graphics g)
        {
            _entities.ForEach(e => e.Render(g));
            if (showNodes)
            {
                foreach (var value in Graph.NodeMap)
                {
                    Graph.Render(g, value.Value);
                }
            }
            
            Target.Render(g);
            _objects.ForEach(o => o.Render(g));
        }
    }
}