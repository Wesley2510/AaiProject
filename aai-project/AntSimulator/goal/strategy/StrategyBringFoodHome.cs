using AntSimulator.entity;

namespace AntSimulator.goal.strategy
{
    public class StrategyBringFoodHome : GoalEvaluator
    {
        public override float CalculateDesirability(Ant ant)
        {
            float score = 0;
            if (!ant.HasFood) return score;
            if (ant.HasFood) score += 500;
            if (ant.FoodLoad > 10) score += 100;
            return score;
        }

        public override void SetGoal(Ant ant)
        {
            if (ant.Brain.Subgoals.Count == 0 || ant.Brain.Subgoals.Peek().GetType() != typeof(GoalGoHome))
            {
                if (ant.Brain.Subgoals.Count != 0) ant.Brain.Subgoals.Peek().SetInactive();
                ant.Brain.AddChild(new GoalGoHome(ant));
            }
        }
    }
}
