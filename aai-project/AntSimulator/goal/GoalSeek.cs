using AntSimulator.behaviour;
using AntSimulator.entity;
using AntSimulator.util;

namespace AntSimulator.goal
{
    class GoalSeek : Goal
    {
        private Vector2D _target;
        private Ant _ant;
        private SteeringBehaviour _seekBehaviour;
        private int _distance;
        public GoalSeek(Ant ant, Vector2D pTarget, int pAllowedDistance) : base(ant)
        {
            _target = pTarget;
            _ant = ant;
            _distance = pAllowedDistance;
            Activate();
        }

        public override void Activate()
        {
            _seekBehaviour = new Seek(_ant, _target);
            _ant.SteeringBehaviours.Add(_seekBehaviour);
            Process();
        }

        public override Status Process()
        {
            if (!IsActive())
            {
                Status = Status.Active;
                Activate();

            }
            if (Vector2D.Distance(_target, _ant.Pos) < _distance)
            {
                Status = Status.Completed;

            }
            return Status;
        }

        public override void Terminate()
        {
            _ant.SteeringBehaviours.Remove(_seekBehaviour);
        }

        public override void AddChild(Goal g)
        {

        }
    }
}
