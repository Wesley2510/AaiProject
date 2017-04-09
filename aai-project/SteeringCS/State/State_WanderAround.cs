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
        private Ant enemy;
        public State_WanderAround(Ant enemy)
        {
            this.enemy = enemy;
        }
        public override void Enter(Ant ant)
        {
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
            if (ant.IsHungry())
            {
                ant.ChangeState(new State_LookForFood(enemy));
            }
        }

        public override void Exit(Ant ant)
        {
            Console.WriteLine("Ant is going to look for food");
        }
    }
}
