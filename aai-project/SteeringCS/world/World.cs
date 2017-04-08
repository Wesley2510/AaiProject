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
using System.Security.Cryptography.X509Certificates;
using SteeringCS.behaviour;
using SteeringCS.entity;
using SteeringCS.states;
using SteeringCS.util;

namespace SteeringCS.world
{
    public class World
    {
        private List<MovingEntity> _entities = new List<MovingEntity>();
        private List<MovingEntity> _badGuys = new List<MovingEntity>();
        public Vehicle Target { get; set; }
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
            Vehicle v = new Vehicle(new Vector2D(200, 150), this) { VColor = Color.Blue };
            _entities.Add(v);

            Target = new Vehicle(new Vector2D(100, 60), this)
            {
                VColor = Color.DarkRed,
                Pos = new Vector2D(400, 400)
            };
        }

        public void Update(float timeElapsed)
        {
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
            Target.Render(g);
        }
    }
}