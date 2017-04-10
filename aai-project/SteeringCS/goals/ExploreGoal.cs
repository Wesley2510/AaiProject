using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.goals
{
    public class ExploreGoal : CompositeGoal<Ant>
    {
        private Vector2D _currentDestination;
        private bool _destinationIsSet;
        private Ant _entitity;
        public ExploreGoal(Ant entity) : base(entity)
        {
            _destinationIsSet = false;
            _entitity = entity;
        }

        public override void Activate()
        {
            Status = GoalState.Active;
            RemoveAllSubGoals();
            if (!_destinationIsSet)
            {
                _currentDestination = _entitity.MyWorld.GetRandomNode();
                _destinationIsSet = true;
            }
            /*_entitity.PathPlanner.RequestPathToPostition(_currentDestination);*/
            AddSubGoal(new SeekToPosition(_entitity, _currentDestination));
        }

        public override GoalState Process()
        {
            ActivateIfInactive();
            Status = ProcessSubgoals();
            return Status;
        }

        public override void Terminate()
        {
            Status = GoalState.Completed;
        }
    }
}
