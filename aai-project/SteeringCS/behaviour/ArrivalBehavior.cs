using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.behaviour
{
    public enum Deceleration { Slow = 3, Normal = 2, Fast = 1 }
    public class ArrivalBehavior : SteeringBehaviour
    {
        public Deceleration Deceleration;
        public ArrivalBehavior(MovingEntity movingEntity, Vector2D target, Deceleration deceleration) : base(movingEntity)
        {
            Target = target;
            Deceleration = deceleration;
        }

        public override Vector2D Calculate()
        {
            Vector2D toTarget = Target - MovingEntity.Pos;
            double distance = Target.Length();

            if (distance > 0)
            {               
                const double decelerationTweaker = 0.3;
                double speed = distance / ((double) Deceleration * decelerationTweaker);
                speed = Math.Min(speed, MovingEntity.MaxSpeed);
                Vector2D desiredVelocity = toTarget * speed / distance;
                return (desiredVelocity - MovingEntity.Velocity);
            }
            return new Vector2D(0,0);
        }
    }
}