using System;
using SteeringCS.behaviour;
using SteeringCS.entity;
using SteeringCS.graphs;
using SteeringCS.world;

namespace SteeringCS.States
{
    class State_RunAway : State
    {
        private Node food;

        public override void Enter(MovingEntity ant, Node food)
        {
            this.food = food;
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
                ant.Steeringbehaviour = new FleeBehavior(movingEntity,World.Target.Pos);
                ant.Hunger -= 0.1f;
            }
            else
            {
                movingEntity.ChangeState(new State_WanderAround(movingEntity));
            }
        }
    }
}