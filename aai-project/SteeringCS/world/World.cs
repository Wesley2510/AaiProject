using SteeringCS.entity;
using SteeringCS.graphs;
using SteeringCS.util;
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

namespace SteeringCS.world
{
    public class World
    {
        private List<MovingEntity> _entities = new List<MovingEntity>();
        private List<Dirt> _objects = new List<Dirt>();
        private List<Node> _nodes = new List<Node>();
        public Ant Target { get; set; }
        public static bool ShowNodes = true;
        public static Ant Enemy { get; set; }
        public Graph Graph = new Graph();
        public int Width { get; set; }
        public int Height { get; set; }
        private PathPlanner _pathPlanner;
        public static Node Food;

        public World(int w, int h)
        {
            Food = new Node(25, new Vector2D(600, 50)) { ExtraInfo = ExtraInfo.HasFood };
            Width = w;
            Height = h;
            Populate();
            BuildGraph(30, 150, 43, 65);
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
            Node tempNode1 = null;

            for (int i = 0; i < numrows; i++)
            {
                for (int k = 0; k < numcolums; k++)
                {
                    Node node = new Node(new Vector2D(tempx + (k * 10), tempy + (i * 10)));

                    if (checkNode(node))
                    {
                        _nodes.Add(node);
                    }
                    else
                    {
                        Console.Write(" FAILED!");
                    }
                }
                int counter = 0;
                foreach (Node node in _nodes)
                {
                    if (tempNode1 == null)
                    {
                        tempNode1 = node;
                    }
                    else
                    {
                        var tempNode2 = tempNode1;
                        tempNode1 = node;

                        if (counter % numrows != 0)
                        {
                            Graph.AddEdge(tempNode2.Index, tempNode2.Postition, tempNode1.Index, tempNode1.Postition,
                                1);
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
            if (x < 0 || y < 0 || x > 800 || y > 600 || Graph.NodeMap.ContainsKey(node.Index))
            {
                return false;
            }
            foreach (Dirt dirt in _objects)
            {
                if (Vector2D.Distance(node.Postition, dirt.Pos) < dirt.Scale + 5)
                {
                    return false;
                }
            }
            return true;
        }

        public Vector2D GetRandomNode()
        {
            Random rnd = new Random();
            return _nodes[rnd.Next(0, _nodes.Count - 1)].Postition;
        }

        public void Update(float timeElapsed)
        {
            foreach (var me in _entities)
            {
                _pathPlanner = new PathPlanner(me, this);
                me.Update(timeElapsed);
            }
        }

        public void Render(Graphics g)
        {
            _entities.ForEach(e => e.Render(g));
            if (ShowNodes)
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