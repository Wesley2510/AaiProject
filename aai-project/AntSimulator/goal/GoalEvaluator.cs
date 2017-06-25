using AntSimulator.entity;

namespace AntSimulator.goal
{
    public abstract class GoalEvaluator
    {
        public abstract float CalculateDesirability(Ant ant);
        public abstract void SetGoal(Ant ant);
    }
}
