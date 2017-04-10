using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.goals
{
    public class MoveToPostitionGoal : CompositeGoal<Ant>
    {
        private Vector2D _destination;
        private Ant _ant;

        public MoveToPostitionGoal(Ant entity, Vector2D pos) : base(entity)
        {
            _destination = pos;
            _ant = entity;
        }

        public override void Activate()
        {
            Status = GoalState.Active;
            RemoveAllSubGoals();
            AddSubGoal(new SeekToPosition(_ant, _destination));
        }

        public override GoalState Process()
        {
            ActivateIfInactive();
            Status = ProcessSubgoals();
            ReactiveIfFailed();
            return Status;
        }

        public override void Terminate()
        {
            Status = GoalState.Completed;
        }
    }
}