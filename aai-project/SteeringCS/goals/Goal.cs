namespace SteeringCS.goals
{
    public abstract class Goal<TEntity>
    {
        public enum GoalState
        {
            Active,
            Inactive,
            Completed,
            Failed
        }

        protected TEntity Entity;
        protected GoalState Status;

        public abstract void Activate();
        public abstract GoalState Process();
        public abstract void Terminate();

        protected Goal(TEntity entity)
        {
            Status = GoalState.Inactive;
            Entity = entity;
        }

        public bool IsComplete()
        {
            return Status == GoalState.Completed;
        }

        public bool IsActive()
        {
            return Status == GoalState.Active;
        }

        public bool IsInactive()
        {
            return Status == GoalState.Inactive;
        }

        public bool HasFailed()
        {
            return Status == GoalState.Failed;
        }

        protected void ActivateIfInactive()
        {
            if (IsInactive())
            {
                Activate();
            }
        }

        protected void ReactiveIfFailed()
        {
            if (HasFailed())
            {
                Status = GoalState.Inactive;
            }
        }
    }
}