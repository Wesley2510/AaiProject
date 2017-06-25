﻿using AntSimulator.entity;

namespace AntSimulator.goal.strategy
{
    public class StrategySitInAntHill : GoalEvaluator
    {
        public override float CalculateDesirability(Ant ant)
        {
            return 0;
        }

        public override void SetGoal(Ant ant)
        {
            if (ant.Brain.Subgoals.Count == 0 || ant.Brain.Subgoals.Peek().GetType() != typeof(GoalSitInAntHill))
            {
                if (ant.Brain.Subgoals.Count != 0) ant.Brain.Subgoals.Peek().SetInactive();
                ant.Brain.AddChild(new GoalSitInAntHill(ant));
            }
        }
    }
}
