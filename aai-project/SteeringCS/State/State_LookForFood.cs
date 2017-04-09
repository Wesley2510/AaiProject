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
        private MovingEntity enemy;
        public State_LookForFood(MovingEntity enemy)
        {
            this.enemy = enemy;
        }
        public override void Enter(MovingEntity movingEntity)
        {
            Console.WriteLine("Ant has started looking for food");
            if (movingEntity.status != MovingEntity.Status.Searching)
            {
                movingEntity.ChangeStatus(MovingEntity.Status.Searching);
            }
        }

        public override void Execute(MovingEntity movingEntity)
        {
            if (movingEntity.Hunger > 0.0f)
            {
                Console.WriteLine("Ant is looking for food");
                movingEntity.Steeringbehaviour = new WanderBehaviour(movingEntity);
                movingEntity.Hunger -= 0.1f;
            }
            else
            {
                movingEntity.ChangeState(new State_WanderAround(enemy));
            }
        }

      
        public override void Exit(MovingEntity movingEntity)
        {
            Console.WriteLine("Ant is done looking for food");
        }
    }
}
