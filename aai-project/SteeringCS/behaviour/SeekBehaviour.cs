using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.behaviour
{
    public class SeekBehaviour : SteeringBehaviour
    {
        public Vector2D Target { get; set; }
        public SeekBehaviour(MovingEntity movingEntity, Vector2D target) : base(movingEntity)
        {
            Target = target;
        }

        public override Vector2D Calculate()
        {
            Vector2D velocity = Vector2D.Normalize(Target - MovingEntity.Pos) * MovingEntity.MaxSpeed;
            Vector2D steering = velocity - MovingEntity.Velocity;
            steering = Vector2D.Truncate(steering, MovingEntity.MaxSpeed);

            return steering;
        }       
    }
}
