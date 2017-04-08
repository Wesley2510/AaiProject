using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;
using SteeringCS.entity;
using SteeringCS.State;
using SteeringCS.util;


namespace SteeringCS.states
{

    public class State_WanderAround : State.State
    {
        private MovingEntity enemy;
        public State_WanderAround(Vehicle enemy)
        {
            this.enemy = enemy;
        }
        public override void Enter(Vehicle ant)
        {
            Console.WriteLine("Ant has started wandering around");
            if (ant.status != Vehicle.Status.Wandering)
            {
                ant.ChangeStatus(Vehicle.Status.Wandering);
            }
        }

        public override void Execute(Vehicle ant)
        {
            ant.IncreaseHunger();
            ant.Steeringbehaviour = new WanderBehaviour(ant);
            Console.WriteLine("Ant is wandering around");
            if (ant.IsHungry())
            {
                ant.ChangeState(new State_LookForFood(enemy));
            }
        }

        public override void Exit(MovingEntity ant)
        {
            Console.WriteLine("Ant is going to look for food");
        }
    }
}
