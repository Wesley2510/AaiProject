using AntSimulator.entity;
using AntSimulator.util;
using System.Diagnostics;

namespace AntSimulator.goal
{
    public class GoalGoHome : CompositeGoal
    {
        private AntHill _antHill;
        public Stopwatch Watch;
        public GoalGoHome(Ant ant) : base(ant)
        {
            _antHill = ant.MyWorld.Anthill;
            Watch = new Stopwatch();
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
                if (!Watch.IsRunning) Watch.Start();
                if (Watch.ElapsedMilliseconds > 1000)
                    Status = Status.Completed;
            }
            else
            {
                if (Watch.IsRunning) Watch.Stop();
            }
            if (Subgoals.Peek().GetType() != typeof(GoalFollowPath) && Vector2D.Distance(_antHill.Pos, Ant.Pos) > 30 + _antHill.Radius)
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

        public override void Terminate()
        {
            if (Ant.HasFood)
                Ant.HasFood = false;
            Ant.FoodLoad = 0;
            base.Terminate();
        }
    }
}
