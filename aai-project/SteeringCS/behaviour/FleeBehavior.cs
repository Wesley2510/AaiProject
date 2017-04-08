using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.behaviour
{
    public class FleeBehavior : SteeringBehaviour

    {
        public Vector2D Target;

        public FleeBehavior(Ant ant, Vector2D target) : base(ant)
        {
            Target = target;
        }

        public override Vector2D Calculate()
        {            
            var seekVelocity = Vector2D.Normalize(Target - Ant.Pos) * Ant.MaxSpeed;
            var steering = seekVelocity - Ant.Velocity;                      
            const double panicDistance = 200.0 * 200.0;
            if (Vector2D.DistanceSquared(Ant.Pos, Target) > panicDistance)
            {
                return steering;
            }            
            var fleeVelocity = seekVelocity * -1;
            var fleeing = fleeVelocity - Ant.Velocity;
            return fleeing;
        }
    }
}