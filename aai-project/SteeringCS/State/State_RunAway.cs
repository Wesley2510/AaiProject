using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;
using SteeringCS.entity;
using SteeringCS.states;
using SteeringCS.util;

namespace SteeringCS.State
{
    class State_RunAway : State
    {
       

        public override void Enter(Vehicle ant)
        {
            Console.WriteLine("Ant is starting to run away");
            if (ant.status != MovingEntity.Status.Fleeing)
            {
                ant.ChangeStatus(Vehicle.Status.Searching);
            }
        }

        public override void Exit(Vehicle ant)
        {
            throw new NotImplementedException();
        }

        public override void Execute(Vehicle ant)
        {
            if (ant.Hunger > 0.0f)
            {
                Console.WriteLine("Ant is running away!");
                //Change target to enemy. 
                ant.Steeringbehaviour = new FleeBehavior(ant, );
                ant.Hunger -= 0.1f;
            }
            else
            {
                ant.ChangeState(new State_WanderAround());
            }
        }
    }
}