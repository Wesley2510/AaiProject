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
        private Ant enemy;
        public State_LookForFood(Ant enemy)
        {
            this.enemy = enemy;
        }
        public override void Enter(Ant ant)
        {
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
                ant.Steeringbehaviour = new WanderBehaviour(ant);
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
