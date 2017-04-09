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

        public override void Enter(Ant ant, Node food)
        {
            this.food = food;
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
                ant.Steeringbehaviour = new FleeBehavior(ant,World.Target.Pos);
                ant.Hunger -= 0.1f;
            }
            else
            {
                ant.ChangeState(new State_WanderAround(ant));
            }
        }
    }
}