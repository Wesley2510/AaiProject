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
       public override void Enter(MovingEntity movingEntity)
        {
            Console.WriteLine("Ant is starting to run away");
            if (movingEntity.status != MovingEntity.Status.Fleeing)
            {
                movingEntity.ChangeStatus(MovingEntity.Status.Searching);
            }
        }

        public override void Exit(MovingEntity movingEntity)
        {
            Console.WriteLine("Ant is done running away");
        }

        public override void Execute(MovingEntity movingEntity)
        {
            if (movingEntity.Hunger > 0.0f)
            {
                Console.WriteLine("Ant is running away!");
                //Change target to enemy. 
                movingEntity.Steeringbehaviour = new FleeBehavior(movingEntity, new Vector2D(10,01));
                movingEntity.Hunger -= 0.1f;
            }
            else
            {
                movingEntity.ChangeState(new State_WanderAround(movingEntity));
            }
        }
    }
}