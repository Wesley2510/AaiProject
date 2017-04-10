using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SteeringCS.goals
{
    public abstract class CompositeGoal<TEntity> : Goal<TEntity>
    {
        protected LinkedList<Goal<TEntity>> SubGoals;

        protected CompositeGoal(TEntity entity) : base(entity)
        {
        }

        protected GoalState ProcessSubgoals()
        {
            while (SubGoals.Any() && (SubGoals.First().IsComplete() || SubGoals.First().HasFailed()))
            {
                SubGoals.RemoveFirst();
            }
            if (!SubGoals.Any()) return GoalState.Completed;
            var statusOfSubGoals = SubGoals.First().Process();
            if (statusOfSubGoals == GoalState.Completed && SubGoals.Count > 1)
            {
                return GoalState.Active;
            }
            return statusOfSubGoals;
        }

        public abstract override void Activate();
        public abstract override GoalState Process();
        public abstract override void Terminate();

        public void AddSubGoal(Goal<TEntity> g)
        {
            SubGoals.AddFirst(g);
        }

        public void RemoveAllSubGoals()
        {
            SubGoals.Clear();
        }

        public virtual void Render(Graphics g)
        {
        }
    }
}