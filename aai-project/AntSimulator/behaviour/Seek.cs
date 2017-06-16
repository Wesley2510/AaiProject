using AntSimulator.entity;
using AntSimulator.util;

namespace AntSimulator.behaviour
{
    public class Seek : SteeringBehaviour
    {
        public Seek(MovingEntity movingentity, Vector2D target) : base(movingentity)
        {
            Target = target;
        }

        public override Vector2D Calculate()
        {
            var desiredVelocity = Vector2D.Normalize(Target - MovingEntity.Pos) * MovingEntity.MaxSpeed;
            return (desiredVelocity - MovingEntity.Velocity).Truncate(MovingEntity.MaxSpeed);
        }
    }
}