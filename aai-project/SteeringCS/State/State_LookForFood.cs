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
    class State_LookForFood : State
    {
        private Vehicle enemy;
        public State_LookForFood(Vehicle enemy)
        {
            this.enemy = enemy;
        }
        public override void Enter(Vehicle ant)
        {
            Console.WriteLine("Ant has started looking for food");
            if (ant.status != Vehicle.Status.Searching)
            {
                ant.ChangeStatus(Vehicle.Status.Searching);
            }
        }

        public override void Execute(Vehicle ant)
        {
            if (ant.Hunger > 0.0f)
            {
                Console.WriteLine("Ant is looking for food");
                ant.Steeringbehaviour = new WanderBehaviour(ant);
                ant.Hunger -= 0.1f;
            }
            else
            {
                ant.ChangeState(new State_WanderAround(enemy));
            }
        }

      
        public override void Exit(Vehicle ant)
        {
            Console.WriteLine("Ant is done looking for food");
        }
    }
}
