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

        public override void Enter(MovingEntity movingEntity, Node food)
        {
            this.food = food;
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

                movingEntity.Steeringbehaviour = new ArrivalBehavior(movingEntity, food.Postition, Deceleration.Fast);
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
