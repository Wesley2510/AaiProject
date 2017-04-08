//using System.Collections.Generic;
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
        }

        private void Populate()
        {
            Vehicle v = new Vehicle(new Vector2D(200, 150), this) { VColor = Color.Blue };
            _entities.Add(v);

            Target = new Vehicle(new Vector2D(100, 60), this)
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
            });
        }

        private void BuildGraph()
        {
            Graph.AddEdge(1, new Vector2D(400,400), 2, new Vector2D(225,175), 1);
            Graph.AddEdge(2, new Vector2D(225, 175), 3, new Vector2D(50, 50), 1);
            Graph.AddEdge(3, new Vector2D(50, 50), 4, new Vector2D(600, 30), 1);
        }

        public void Update(float timeElapsed)
        {
            foreach (var me in _entities)
            {
                
                for (int i = 1; i < Graph.NodeMap.Count; i++)
                {
                    //me.Steeringbehaviour = new FleeBehavior(me, Target.Pos);
                    Graph.Astar(1,2);
                    me.Steeringbehaviour = new ArrivalBehavior(me, Graph.NodeMap[i].Postition, Deceleration.Fast);
                    //me.Steeringbehaviour = new SeekBehaviour(me, Graph.NodeMap[i].Postition);    
                }

            foreach (var me in _entities)
            {

                if (me.CurrentState != null)
                {
                    me.CurrentState.Execute(me);
                }
                else
                {
                    me.ChangeState(new State_WanderAround(target));
                }

                //me.Steeringbehaviour = new FleeBehavior(me, Target.Pos);
                //me.Steeringbehaviour = new ArrivalBehavior(me, Target.Pos, Deceleration.Slow);
                //me.Steeringbehaviour = new SeekBehaviour(me, Target.Pos);
                //me.Steeringbehaviour = new WanderBehaviour(me);
                me.Update(timeElapsed);
            }
        }

        public void Render(Graphics g)
        {
            _entities.ForEach(e => e.Render(g));
            foreach (var value in Graph.NodeMap)
            {
                Graph.Render(g, value.Value);
            }
            Target.Render(g);
            Objects.ForEach(o => o.Render(g));
        }
    }
}