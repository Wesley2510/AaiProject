using System;
using SteeringCS.behaviour;
using SteeringCS.entity;
using SteeringCS.graphs;

namespace SteeringCS.States
{

    public class State_WanderAround : State
    {
        private Ant enemy;
        private Node food;
        public State_WanderAround(Ant enemy)
        {
            this.enemy = enemy;
        }
        public override void Enter(Ant ant, Node food )
        {
            this.food = food;
            Console.WriteLine("Ant has started wandering around");
            if (ant.status != Ant.Status.Wandering)
            {
                ant.ChangeStatus(Ant.Status.Wandering);
            }
        }

        public override void Execute(Ant ant)
        {
            ant.IncreaseHunger();
            ant.Steeringbehaviour = new WanderBehaviour(ant);
            Console.WriteLine("Ant is wandering around");
            if (ant.IsHungry() )
            {
                ant.ChangeState(new State_LookForFood());
            } 
            //else if (!ant.IsSafe())
            //{
            //    ant.ChangeState(new State_RunAway());
            //}
        }

        public override void Exit(Ant ant)
        {
            Console.WriteLine("Ant is going to look for food");
        }
    }
}
