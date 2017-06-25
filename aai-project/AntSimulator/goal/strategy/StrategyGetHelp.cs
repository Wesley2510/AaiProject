using AntSimulator.entity;

namespace AntSimulator.goal.strategy
{
    public class StrategyGetHelp : GoalEvaluator
    {
        public override float CalculateDesirability(Ant ant)
        {
            float score = 0;
            if (ant.WorkLoad >= 25) ant.WantsHelp = true;
            if (ant.WantsHelp) score = 10000;
            return score;
        }

        public override void SetGoal(Ant ant)
        {
            if (ant.Brain.Subgoals.Count == 0 || ant.Brain.Subgoals.Peek().GetType() != typeof(GoalGetHelp))
            {
                if (ant.Brain.Subgoals.Count != 0) ant.Brain.Subgoals.Peek().SetInactive();
                ant.Brain.AddChild(new GoalGetHelp(ant));
            }
        }
    }
}
