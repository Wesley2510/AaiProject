using AntSimulator.entity;
using AntSimulator.util;
using System.Collections.Generic;

namespace AntSimulator.goal
{
    public class GoalFollowPath : CompositeGoal
    {
        protected List<Vector2D> Path;
        public Vector2D Target;

        public GoalFollowPath(Ant ant, Vector2D target) : base(ant)
        {
            Target = target;
        }

        public override void Activate()
        {
            Ant.MyWorld.Graph.GetRoute(Ant.Pos, Target, out Path);
            Subgoals.Push(new GoalArrival(Ant, Path[0], 5));
            for (var index = 1; index < Path.Count; index++)
            {
                Vector2D vector2D = Path[index];
                Subgoals.Push(new GoalSeek(Ant, vector2D, 20));
            }
        }

        public override Status Process()
        {
            if (!IsActive())
            {
                Activate();
                Status = Status.Active;
            }
            ProcessSubGoals();
            if (Subgoals.Count != 0) return Status;
            Status = Status.Completed;

            return Status;
        }
    }

}
