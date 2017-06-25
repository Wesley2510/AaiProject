using AntSimulator.behaviour;
using AntSimulator.entity;
using AntSimulator.util;

namespace AntSimulator.goal
{
    class GoalArrival : Goal
    {
        private Vector2D _target;
        private BaseGameEntity _targetEntity;
        private Arrival _attachBehaviour;
        private int _deviationAllowance;

        public GoalArrival(Ant ant, Vector2D target, int deviationAllowance) : base(ant)
        {
            _target = target;
            _deviationAllowance = deviationAllowance;
        }

        public GoalArrival(Ant ant, BaseGameEntity targetEntity, int deviationAllowance) : base(ant)
        {
            _deviationAllowance = deviationAllowance;
            _targetEntity = targetEntity;
        }

        public override void Activate()
        {
            Vector2D target = _targetEntity != null ? _targetEntity.Pos : _target;
            _attachBehaviour = new Arrival(Ant, target);
            Ant.SteeringBehaviours.Add(_attachBehaviour);
        }

        public override Status Process()
        {
            if (!IsActive())
            {
                Activate();
                Status = Status.Active;
            }
            Vector2D target = _targetEntity != null ? _targetEntity.Pos : _target;
            _attachBehaviour.Target = target;
            if (Vector2D.Distance(target, Ant.Pos) < _deviationAllowance)
            {
                Status = Status.Completed;
            }
            return Status;
        }

        public override void Terminate()
        {
            Ant.SteeringBehaviours.Remove(_attachBehaviour);
        }

        public override void AddChild(Goal g)
        {
        }
    }
}
