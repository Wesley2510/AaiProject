using AntSimulator.entity;

namespace AntSimulator.goal
{
    public abstract class CompositeGoal : Goal
    {
        protected CompositeGoal(Ant ant) : base(ant)
        {
        }

        public override void AddChild(Goal g)
        {
            Subgoals.Push(g);
        }

        public override void Terminate()
        {
            foreach (Goal g in Subgoals)
            {
                g.Terminate();
            }
        }

        public Status ProcessSubGoals()
        {
            while (Subgoals.Count != 0 && (Subgoals.Peek().IsComplete() || Subgoals.Peek().HasFailed()))
            {
                Subgoals.Peek().Terminate();
                Subgoals.Pop();
            }
            if (Subgoals.Count > 0)
            {
                Status StatusOfSubGoals = Subgoals.Peek().Process();
                if (StatusOfSubGoals == Status.Completed && Subgoals.Count > 1)
                {
                    return Status.Active;
                }
                return StatusOfSubGoals;
            }
            return Status.Completed;
        }


        public override void SetInactive()
        {
            Terminate();
            base.SetInactive();
        }
    }
}