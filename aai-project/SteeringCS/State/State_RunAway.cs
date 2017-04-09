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
       

        public override void Enter(Ant ant)
        {
            Console.WriteLine("Ant is starting to run away");
            if (ant.status != Ant.Status.Fleeing)
            {
                ant.ChangeStatus(Ant.Status.Searching);
            }
        }

        public override void Exit(Ant ant)
        {
            Console.WriteLine("Ant is done running away");
        }

        public override void Execute(Ant ant)
        {
            if (ant.Hunger > 0.0f)
            {
                Console.WriteLine("Ant is running away!");
                //Change target to enemy. 
                ant.Steeringbehaviour = new FleeBehavior(ant, new Vector2D(10,01));
                ant.Hunger -= 0.1f;
            }
            else
            {
                ant.ChangeState(new State_WanderAround(ant));
            }
        }
    }
}