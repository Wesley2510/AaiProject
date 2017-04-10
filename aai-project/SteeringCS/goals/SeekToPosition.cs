using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;
using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.goals
{
    public class SeekToPosition : Goal<Ant>
    {
        private Vector2D _position;
        private Ant _ant;

        private bool IsStuck()
        {
            throw new NotImplementedException();
        }

        public SeekToPosition(Ant entity, Vector2D target) : base(entity)
        {
            _position = target;
            _ant = entity;
        }

        public override void Activate()
        {
            Status = GoalState.Active;
            _ant.Steeringbehaviour.Target = _position;
            _ant.Steeringbehaviour = new SeekBehaviour(_ant, _position);
        }

        public override GoalState Process()
        {
            ActivateIfInactive();
            if (IsStuck())
            {
                Status = GoalState.Failed;
            }
            else
            {
                if (_ant.IsAtPosition(_position))
                    Status = GoalState.Completed;
            }
            return Status;
        }

        public override void Terminate()
        {
            _ant.Steeringbehaviour = new StandStill(_ant);
            Status = GoalState.Completed;
        }
    }
}