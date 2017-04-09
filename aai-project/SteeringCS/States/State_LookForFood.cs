using System;
using SteeringCS.behaviour;
using SteeringCS.entity;
using SteeringCS.graphs;

namespace SteeringCS.States
{
    class State_LookForFood : State
    {
        private Ant enemy;
        private Node food;
       
        public override void Enter(Ant ant , Node food)
        {
            this.food = food;
            Console.WriteLine("Ant has started looking for food");
            if (ant.status != Ant.Status.Searching)
            {
                ant.ChangeStatus(Ant.Status.Searching);
            }
        }

        public override void Execute(Ant ant)
        {
            if (ant.Hunger > 0.0f)
            {
                Console.WriteLine("Ant is looking for food");
                
                ant.Steeringbehaviour = new ArrivalBehavior(ant, food.Postition, Deceleration.Fast);
                ant.Hunger -= 0.1f;
            }
            else
            {
                ant.ChangeState(new State_WanderAround(enemy));
            }
        }

      
        public override void Exit(Ant ant)
        {
            Console.WriteLine("Ant is done looking for food");
        }
    }
}
