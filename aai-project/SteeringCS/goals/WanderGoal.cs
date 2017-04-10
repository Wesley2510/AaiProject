using System;
using SteeringCS.behaviour;
using SteeringCS.entity;

namespace SteeringCS.goals
{
    public class WanderGoal : Goal<Ant>
    {
        private Ant _ant;

        public WanderGoal(Ant entity) : base(entity)
        {
            _ant = entity;
        }

        public override void Activate()
        {
            Status = GoalState.Active;
            _ant.Steeringbehaviour = new WanderBehaviour(_ant);
        }

        public override GoalState Process()
        {
            ActivateIfInactive();
            return Status;
        }

        public override void Terminate()
        {
            _ant.Steeringbehaviour = new StandStill(_ant);
        }
    }
}