using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.behaviour
{
    public class FleeBehavior : SteeringBehaviour

    {
        public Vector2D Target;

        public FleeBehavior(MovingEntity movingEntity, Vector2D target) : base(movingEntity)
        {
            Target = target;
        }

        public override Vector2D Calculate()
        {
            Vector2D preNormalisedVelocity = Target - MovingEntity.Pos;
            Vector2D desiredVelocity = preNormalisedVelocity.Normalize() * MovingEntity.MaxSpeed;
            Vector2D fleeDesiredVelocity = desiredVelocity * -1;
            Vector2D steering = fleeDesiredVelocity - MovingEntity.Velocity;
            return steering;

        }
    }
}