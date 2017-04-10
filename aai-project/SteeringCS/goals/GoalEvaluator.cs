using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.goals
{
    public abstract class GoalEvaluator
    {
        protected double CharacterBias;

        protected GoalEvaluator(double characterBias)
        {
            CharacterBias = characterBias;
        }

        public abstract double CalculateDesirability(Ant ant);
        public abstract void SetGoal(Ant ant);
        public abstract void RenderInfo(Vector2D position, Ant ant);
    }
}