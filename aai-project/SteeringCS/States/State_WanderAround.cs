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
        public override void Enter(MovingEntity movingEntity Node food )
        {
            this.food = food;
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
