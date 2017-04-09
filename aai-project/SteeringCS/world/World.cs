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
//        public Vehicle Target { get; set; }
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
//            Vehicle v = new Vehicle(new Vector2D(200, 150), this) {VColor = Color.Blue};
//            //v.ChangeState(new State_LookForFood());
//            _entities.Add(v);

//            Target = new Vehicle(new Vector2D(100, 60), this)
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
using SteeringCS.behaviour;
using SteeringCS.entity;
using SteeringCS.graphs;
using SteeringCS.states;
using SteeringCS.util;

namespace SteeringCS.world
{
    public class World
    {
        private List<Ant> _entities = new List<Ant>();
        public Vehicle Target { get; set; }
        public List<Vehicle> Objects = new List<Vehicle>();
        public Graph Graph = new Graph();
        public int Width { get; set; }
        public int Height { get; set; }

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            Populate();
            BuildGraph();
            Graph.Astar(9,25);
            Graph.PrintPath(25);
            
        }

        private void Populate()
        {
            Vehicle v = new Vehicle(new Vector2D(50, 225), this) {VColor = Color.Black, DrawType = DrawType.Fill};
            _entities.Add(v);

            /*Target = new Vehicle(new Vector2D(100, 60), this)
            {
                VColor = Color.DarkRed,
                Pos = new Vector2D(400, 400)
            };
            Objects.Add(new Vehicle(new Vector2D(700, 100), this)
            {
                VColor = Color.Black,
                Scale = 50,
                DrawType = DrawType.Fill
            });
            Objects.Add(new Vehicle(new Vector2D(300, 70), this)
            {
                VColor = Color.Black,
                Scale = 10,
                DrawType = DrawType.Fill
            });*/
        }

        private void BuildGraph()
        {
            Graph.AddEdge(1, new Vector2D(50, 225), 2, new Vector2D(100, 260), 1);
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
            Graph.AddEdge(24, new Vector2D(245, 520), 25, new Vector2D(200, 580), 1);
            //Graph.AddEdge(0, new Vector2D(0, 0), 0, new Vector2D(0, 0), 1);
        }

        public void Update(float timeElapsed)
        {
            //foreach (var me in _entities)
            //{

            //    for (int i = 1; i < Graph.NodeMap.Count; i++)
            //    {
            //        //me.Steeringbehaviour = new FleeBehavior(me, Target.Pos);
            //        Graph.Astar(1, 2);
            //        me.Steeringbehaviour = new ArrivalBehavior(me, Graph.NodeMap[i].Postition, Deceleration.Fast);
            //        //me.Steeringbehaviour = new SeekBehaviour(me, Graph.NodeMap[i].Postition);    
            //    }

                foreach (var me in _entities)
                {

                    if (me.CurrentState != null)
                    {
                        me.CurrentState.Execute(me);
                    }
                    else
                    {
                        me.ChangeState(new State_WanderAround(Target));
                    }
                    //me.Steeringbehaviour = new FleeBehavior(me, Target.Pos);
                    //me.Steeringbehaviour = new ArrivalBehavior(me, Target.Pos, Deceleration.Slow);
                    //me.Steeringbehaviour = new SeekBehaviour(me, Target.Pos);
                    //me.Steeringbehaviour = new WanderBehaviour(me);
                    me.Update(timeElapsed);
                }
           // }
        }

        public void Render(Graphics g)
        {
            _entities.ForEach(e => e.Render(g));
            foreach (var value in Graph.NodeMap)
            {
                Graph.Render(g, value.Value);
            }
            //Target.Render(g);
            Objects.ForEach(o => o.Render(g));
        }
    }
}