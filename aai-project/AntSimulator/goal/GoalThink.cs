using AntSimulator.entity;
using AntSimulator.goal.strategy;
using System.Collections.Generic;

namespace AntSimulator.goal
{
    public class GoalThink : CompositeGoal
    {
        private List<GoalEvaluator> _evaluators;
        public GoalThink(Ant ant) : base(ant)
        {
            _evaluators = new List<GoalEvaluator>
            {
                new StrategySitInAntHill(),
                new StrategyGetFood(),
                new StrategyBringFoodHome(),
                new StrategySateThirst(),
                new StrategyGetHelp(),

            };
        }

        public override void Activate()
        {
        }

        public override Status Process()
        {
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
