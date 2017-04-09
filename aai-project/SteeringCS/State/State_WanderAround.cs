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
        public State_WanderAround(MovingEntity enemy)
        {
            this.enemy = enemy;
        }
        public override void Enter(MovingEntity movingEntity)
        {
            Console.WriteLine("Ant has started wandering around");
            if (movingEntity.status != MovingEntity.Status.Wandering)
            {
                movingEntity.ChangeStatus(MovingEntity.Status.Wandering);
            }
        }

        public override void Execute(MovingEntity movingEntity)
        {
            movingEntity.IncreaseHunger();
            movingEntity.Steeringbehaviour = new WanderBehaviour(movingEntity);
            Console.WriteLine("Ant is wandering around");
            if (movingEntity.IsHungry())
            {
                movingEntity.ChangeState(new State_LookForFood(enemy));
            }
        }

        public override void Exit(MovingEntity movingEntity)
        {
            Console.WriteLine("Ant is going to look for food");
        }
    }
}
