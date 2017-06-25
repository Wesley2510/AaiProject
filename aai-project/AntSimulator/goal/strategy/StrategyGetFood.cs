using AntSimulator.entity;

namespace AntSimulator.goal.strategy
{
    public class StrategyGetFood : GoalEvaluator
    {
        public override float CalculateDesirability(Ant ant)
        {
            float score = 0;
            if (ant.HasFood) return score;
            if (ant.WorkLoad <= 50) score += 500;
            if (ant.Thirst > 50) score -= 100;
            if (ant.Thirst < 50) score += 500;
            if (ant.WorkLoad > 50) score -= 250;
            return score * ant.GetFoodDesirability();
        }

        public override void SetGoal(Ant ant)
        {
            if (ant.Brain.Subgoals.Count == 0 || ant.Brain.Subgoals.Peek().GetType() != typeof(GoalGetFood))
            {
                if (ant.Brain.Subgoals.Count != 0) ant.Brain.Subgoals.Peek().SetInactive();
                ant.Brain.AddChild(new GoalGetFood(ant));
            }
        }
    }
}
