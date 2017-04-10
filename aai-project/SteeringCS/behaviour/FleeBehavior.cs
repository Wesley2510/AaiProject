using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.behaviour
{
    public class FleeBehavior : SteeringBehaviour

    {
        public FleeBehavior(MovingEntity movingEntity, Vector2D target) : base(movingEntity)
        {
            Target = target;
        }

        public override Vector2D Calculate()
        {            
            var seekVelocity = Vector2D.Normalize(Target - MovingEntity.Pos) * MovingEntity.MaxSpeed;
            var steering = seekVelocity - MovingEntity.Velocity;                      
            const double panicDistance = 200.0 * 200.0;
            if (Vector2D.DistanceSquared(MovingEntity.Pos, Target) > panicDistance)
            {
                return steering;
            }            
            var fleeVelocity = seekVelocity * -1;
            var fleeing = fleeVelocity - MovingEntity.Velocity;
            return fleeing;
        }
    }
}