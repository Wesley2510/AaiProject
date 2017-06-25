using AntSimulator.entity;
using AntSimulator.util;

namespace AntSimulator.goal
{
    public class GoalGetFood : CompositeGoal
    {
        private Food _foodTarget;

        public GoalGetFood(Ant ant) : base(ant)
        {
        }

        public override void Activate()
        {
            double distance = 0;
            Food closest = null;
            foreach (var food in Ant.MyWorld.Food)
            {
                if (food.GetType() == typeof(Food))
                {
                    var currentdistance = Vector2D.Distance(Ant.Pos, food.Pos);
                    if (!(currentdistance < distance) && closest != null) continue;
                    distance = currentdistance;
                    closest = food;
                }
            }
            _foodTarget = closest;
            AddChild(new GoalIdle(Ant));
            AddChild(new GoalArrival(Ant, _foodTarget, 5));
            AddChild(new GoalFollowPath(Ant, _foodTarget.Pos));
            Status = Status.Active;
        }

        public override Status Process()
        {
            if (!IsActive())
            {
                Activate();
                Status = Status.Active;
            }
            Status = ProcessSubGoals();
            if (Subgoals.Peek().GetType() == typeof(GoalIdle))
                if (Vector2D.Distance(_foodTarget.Pos, Ant.Pos) > _foodTarget.Radius)
                {
                    AddChild(new GoalArrival(Ant, _foodTarget, 5));
                    Ant.FoodLoad += 5;
                    Ant.HasFood = true;
                    Status = Status.Completed;
                }
            if (Subgoals.Peek().GetType() != typeof(GoalFollowPath) &&
                Vector2D.Distance(_foodTarget.Pos, Ant.Pos) > 30 + _foodTarget.Radius)
                AddChild(new GoalFollowPath(Ant, _foodTarget.Pos));
            if (Subgoals.Peek().GetType() == typeof(GoalFollowPath))
            {
                GoalFollowPath current = (GoalFollowPath)Subgoals.Peek();
                if (current.Target != _foodTarget.Pos)
                {
                    Subgoals.Pop().Terminate();
                    AddChild(new GoalFollowPath(Ant, _foodTarget.Pos));
                }
            }
            return Status;
        }

        public override void Terminate()
        {

        }
    }
}
