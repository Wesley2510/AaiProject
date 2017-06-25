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
        private int _deviationallowance;
        private Ant _ant;

        public GoalArrival(Ant ant, Vector2D target, int pDeviationallowance) : base(ant)
        {
            _target = target;
            _ant = ant;
            _deviationallowance = pDeviationallowance;
            Activate();
        }

        public override void Activate()
        {
            Vector2D Target = _targetEntity != null ? _targetEntity.Pos : _target;
            _attachBehaviour = new Arrival(_ant, Target, Deceleration.Fast);
            _ant.SteeringBehaviours.Add(_attachBehaviour);
            //Process();
        }

        public override Status Process()
        {
            if (!IsActive())
            {
                Activate();
                Status = Status.Active;
            }
            Vector2D Target = _targetEntity != null ? _targetEntity.Pos : _target;
            _attachBehaviour.Target = Target;
            if (Vector2D.Distance(Target, _ant.Pos) < _deviationallowance)
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
