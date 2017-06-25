using AntSimulator.entity;
using AntSimulator.goal.strategy;
using System.Collections.Generic;

namespace AntSimulator.goal
{
    public class AntThinkGoal : CompositeGoal
    {
        private List<GoalEvaluator> _evaluators;
        public AntThinkGoal(Ant ant) : base(ant)
        {
            _evaluators = new List<GoalEvaluator>
            {
                new StrategyBringFoodHome(),
                new StrategySateThirst()
            };
        }

        public override void Activate()
        {
        }

        public override Status Process()
        {
            Ant.WorkLoad += 1;
            if (!IsActive())
            {
                Status = Status.Active;
                Activate();
            }
            Status = ProcessSubGoals();
            Arbitrate();
            return Status;
        }

        private void Arbitrate()
        {
            float best = 0;
            GoalEvaluator mostDesirable = null;
            foreach (GoalEvaluator evaluator in _evaluators)
            {
                float desirability = evaluator.CalculateDesirability(Ant);
                if (desirability > best)
                {
                    best = desirability;
                    mostDesirable = evaluator;
                }
            }
            mostDesirable?.SetGoal(Ant);
        }
    }
}
