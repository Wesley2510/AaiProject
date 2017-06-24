using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntSimulator.behaviour;
using AntSimulator.entity;
using AntSimulator.util;
using AntSimulator.world;

namespace AntSimulator.Goals
{
    class GoalGetFood : CompositeGoal
    {
        private Vector2D _target;
        private MovingEntity _me;
        private SteeringBehaviour _seekBehaviour;
        private int _distance;
        
        public GoalGetFood(MovingEntity me) : base(me)
        {
           List<Food> foods = me.MyWorld.GetNearbyFood(1000, me.Pos);
            foreach (var food in foods)
            {
                Console.Write(food);
            }
        }

        public override void Activate()
        {
            _seekBehaviour = new Seek(_me, _target);
            _me.SteeringBehaviours.Add(_seekBehaviour);
            Process();
        }

        public override Status Process()
        {
            throw new NotImplementedException();
        }

        public override void Terminate()
        {

        }
    }
}
