using AntSimulator.entity;
using AntSimulator.util;
using System.Diagnostics;

namespace AntSimulator.goal
{
    public class GoalSateThirst : CompositeGoal
    {
        private Water _water;
        private Stopwatch _watch;

        public GoalSateThirst(Ant ant) : base(ant)
        {
            _water = ant.MyWorld.Water;
            _watch = new Stopwatch();
        }

        public override void Activate()
        {
            AddChild(new GoalIdle(Ant));
            AddChild(new GoalArrival(Ant, _water, _water.Radius));
            AddChild(new GoalFollowPath(Ant, _water.Pos));
            Status = Status.Active;
        }

        public override Status Process()
        {
            if (!IsActive())
                Activate();
            Status = ProcessSubGoals();
            if (Subgoals.Peek().GetType() == typeof(GoalIdle))
            {
                double distance = Vector2D.Distance(_water.Pos, Ant.Pos);
                if (Vector2D.Distance(_water.Pos, Ant.Pos) > _water.Radius)
                {
                    AddChild(new GoalArrival(Ant, _water, _water.Radius / 2));
                }
                if (!_watch.IsRunning) _watch.Start();
                if (_watch.ElapsedMilliseconds > 1000)
                {
                    Status = Status.Completed;
                }
            }
            else
            {
                if (_watch.IsRunning) _watch.Stop();
            }
            if (Subgoals.Peek().GetType() != typeof(GoalFollowPath) &&
                Vector2D.Distance(_water.Pos, Ant.Pos) > 30 + _water.Radius)
            {
                AddChild(new GoalFollowPath(Ant, _water.Pos));
            }
            if (Subgoals.Peek().GetType() == typeof(GoalFollowPath))
            {
                var goal = (GoalFollowPath)Subgoals.Peek();
                if (goal.Target != _water.Pos)
                {
                    Subgoals.Pop().Terminate();
                    AddChild(new GoalFollowPath(Ant, _water.Pos));
                }
            }
            return Status;
        }

        public override void Terminate()
        {
            Ant.Thirst = 0;
            base.Terminate();
        }
    }
}
