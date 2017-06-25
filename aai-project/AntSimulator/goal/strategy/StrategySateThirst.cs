using AntSimulator.entity;

namespace AntSimulator.goal.strategy
{
    public class StrategySateThirst : GoalEvaluator
    {
        public override float CalculateDesirability(Ant ant)
        {
            float score = 0;
            if (ant.Thirst > 50) score += 1000;
            return score;
        }

        public override void SetGoal(Ant ant)
        {
            if (ant.Brain.Subgoals.Count == 0 || ant.Brain.Subgoals.Peek().GetType() != typeof(GoalSateThirst))
            {
                if (ant.Brain.Subgoals.Count != 0) ant.Brain.Subgoals.Peek().SetInactive();
                ant.Brain.AddChild(new GoalSateThirst(ant));
            }
        }
    }
}
