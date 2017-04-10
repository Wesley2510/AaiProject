using SteeringCS.entity;
using SteeringCS.graphs;
using SteeringCS.util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SteeringCS.goals
{
    public class ThinkGoal : CompositeGoal<Ant>
    {
        private const double LowRangeBias = 0.5;
        private const double HighRangeBias = 1.5;
        private List<GoalEvaluator> _evaluators = new List<GoalEvaluator>();
        private List<CompositeGoal<Ant>> _goals = new List<CompositeGoal<Ant>>();
        private static Random _rnd = new Random();
        private double _bias = _rnd.NextDouble(LowRangeBias, HighRangeBias);
        private Ant _ant;


        public ThinkGoal(Ant entity) : base(entity)
        {
            _ant = entity;
            _goals.Add(new MoveToPostitionGoal(_ant, new Vector2D(_rnd.Next(100, 600), _rnd.Next(100, 600))));
            _goals.Add(new ExploreGoal(_ant));
        }

        public override void Activate()
        {
            Arbitrate();
            Status = GoalState.Active;
        }


        public override GoalState Process()
        {
            ActivateIfInactive();
            GoalState subgoalStatus = ProcessSubgoals();
            if (subgoalStatus == GoalState.Completed || subgoalStatus == GoalState.Failed)
            {
                Status = GoalState.Inactive;
            }
            return Status;
        }

        public void Arbitrate()
        {
            double best = 0;
            GoalEvaluator MostDesirable = null;
            foreach (var curDes in _evaluators)
            {
                double desirability = curDes.CalculateDesirability(_ant);
                if (desirability >= best)
                {
                    best = desirability;
                    MostDesirable = curDes;
                }
            }
            MostDesirable?.SetGoal(_ant);
        }

        public override void Terminate()
        {
            Status = GoalState.Completed;
        }

        public bool NotPresent(Type state)
        {
            if (!SubGoals.Any())
            {
                return SubGoals.First.GetType() != state;
            }
            return true;
        }

        public void AddGoal_MoveToPostition(Vector2D position)
        {
            AddSubGoal(new MoveToPostitionGoal(_ant, position));
        }

        public void AddGoal_GetItem(ExtraInfo itemType)
        {
            /*if (SubGoals.First.Equals(_ant))
            {*/
            RemoveAllSubGoals();
            AddSubGoal(new GetItemGoal(_ant, itemType));
            /*}*/
        }

        private ExtraInfo ItemTypeToGoalType(ExtraInfo itemType)
        {
            throw new NotImplementedException();
        }

        public void AddGoal_Explore()
        {
        }
    }

    #region RandomExtensions

    public static class RandomExtensions
    {
        public static double NextDouble(
            this Random random,
            double minValue,
            double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }

    #endregion
}