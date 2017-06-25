using AntSimulator.entity;
using AntSimulator.util;

namespace AntSimulator.goal
{
    public class GoalSitInAntHill : CompositeGoal
    {
        private AntHill _antHill;
        public GoalSitInAntHill(Ant ant) : base(ant)
        {
            _antHill = ant.MyWorld.Anthill;
            Activate();
        }

        public override void Activate()
        {
            AddChild(new GoalIdle(Ant));
            AddChild(new GoalArrival(Ant, _antHill, _antHill.Radius));
            AddChild(new GoalFollowPath(Ant, _antHill.Pos));
            Status = Status.Active;
        }

        public override Status Process()
        {
            if (!IsActive())
                Activate();
            Status = ProcessSubGoals();
            if (Subgoals.Peek().GetType() == typeof(GoalIdle))
            {
                if (Vector2D.Distance(_antHill.Pos, Ant.Pos) > _antHill.Radius)
                    AddChild(new GoalArrival(Ant, _antHill, _antHill.Radius / 2));
            }
            if (Subgoals.Peek().GetType() != typeof(GoalFollowPath) && Vector2D.Distance(_antHill.Pos, Ant.Pos) > _antHill.Radius)
                AddChild(new GoalFollowPath(Ant, _antHill.Pos));
            if (Subgoals.Peek().GetType() == typeof(GoalFollowPath))
            {
                var goal = (GoalFollowPath)Subgoals.Peek();
                if (goal.Target != _antHill.Pos)
                {
                    Subgoals.Pop().Terminate();
                    AddChild(new GoalFollowPath(Ant, _antHill.Pos));
                }
            }
            return Status;
        }
    }
}
