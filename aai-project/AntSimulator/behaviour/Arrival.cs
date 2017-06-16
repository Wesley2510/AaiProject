using AntSimulator.entity;
using AntSimulator.util;
using System;

namespace AntSimulator.behaviour
{
    public class Arrival : SteeringBehaviour
    {
        private Deceleration deceleration;
        public Arrival(MovingEntity movingentity, Vector2D target, Deceleration decel) : base(movingentity)
        {
            Target = target;
            deceleration = decel;
        }

        public override Vector2D Calculate()
        {
            Vector2D toTarget = Target - MovingEntity.Pos;
            double dist = Target.Length();
            if (dist > 0)
            {
                const double decelerationTweaker = 0.3;
                double speed = dist / ((double)deceleration * decelerationTweaker);
                speed = Math.Min(speed, MovingEntity.MaxSpeed);
                var desiredvelocity = toTarget * speed / dist;
                return (desiredvelocity - MovingEntity.Velocity);
            }
            return new Vector2D(0, 0);
        }
    }
}