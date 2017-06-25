using AntSimulator.entity;

namespace AntSimulator.goal
{
    public class GoalIdle : Goal
    {
        public GoalIdle(Ant ant) : base(ant)
        {
        }

        public override void Activate()
        {
        }

        public override Status Process()
        {
            if (!IsActive())
            {
                Status = Status.Active;
                Activate();
            }
            return Status.Active;
        }

        public override void Terminate()
        {
        }

        public override void AddChild(Goal g)
        {
        }
    }
}
