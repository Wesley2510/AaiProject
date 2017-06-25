using AntSimulator.behaviour;
using AntSimulator.entity;
using AntSimulator.util;

namespace AntSimulator.goal
{
    class GoalSeek : Goal
    {
        private Vector2D _target;
        private BaseGameEntity _targetEntity;
        private SteeringBehaviour _seekBehaviour;
        private int _distance;

        public GoalSeek(Ant ant, Vector2D target, int deviationAllowance) : base(ant)
        {
            _target = target;
            _distance = deviationAllowance;
        }

        public GoalSeek(Ant ant, BaseGameEntity target, int deviationAllowance) : base(ant)
        {
            _targetEntity = target;
            _distance = deviationAllowance;
        }

        public override void Activate()
        {
            _seekBehaviour = new Seek(Ant, _target);
            Ant.SteeringBehaviours.Add(_seekBehaviour);
        }

        public override Status Process()
        {
            if (!IsActive())
            {
                Status = Status.Active;
                Activate();

            }
            if (Vector2D.Distance(_target, Ant.Pos) < _distance)
            {
                Status = Status.Completed;

            }
            return Status;
        }

        public override void Terminate()
        {
            Ant.SteeringBehaviours.Remove(_seekBehaviour);
        }

        public override void AddChild(Goal g)
        {

        }
    }
}
