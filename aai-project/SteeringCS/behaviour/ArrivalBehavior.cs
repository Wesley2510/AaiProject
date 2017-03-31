using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.behaviour
{
    public class ArrivalBehavior : SteeringBehaviour
    {
        public Vector2D Target;

        public ArrivalBehavior(MovingEntity movingEntity, Vector2D target) : base(movingEntity)
        {
            Target = target;
        }

        public override Vector2D Calculate()
        {
            
            var desiredVelocity = Target - MovingEntity.Pos;
            var distance = Vector2D.Length(desiredVelocity);
            const double slowingRadius = 100.0 * 100.0;

            if (distance > slowingRadius)
            {
                desiredVelocity = Vector2D.Normalize(desiredVelocity) * MovingEntity.MaxSpeed * (distance / slowingRadius);
            }
            else
            {
                desiredVelocity = Vector2D.Normalize(desiredVelocity) * MovingEntity.MaxSpeed;
            }
            Vector2D steering =  desiredVelocity - MovingEntity.Velocity;
            Vector2D velocity = Vector2D.Truncate(MovingEntity.Velocity + steering, MovingEntity.MaxSpeed);
            Vector2D position = velocity - steering;
            return position;
        }
    }
}