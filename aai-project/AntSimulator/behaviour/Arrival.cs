using AntSimulator.entity;
using AntSimulator.util;
using System;

namespace AntSimulator.behaviour
{
    public enum Deceleration
    {
        Slow = 2,
        Normal = 4,
        Fast = 6
    }
    public class Arrival : SteeringBehaviour
    {
        private Deceleration _deceleration;
        public Arrival(MovingEntity movingentity, Vector2D target) : base(movingentity)
        {
            Target = target;
            _deceleration = Deceleration.Fast;
        }

        public override Vector2D Calculate()
        {
            Vector2D toTarget = Target - MovingEntity.Pos;
            double dist = toTarget.Length();
            if (dist > 0)
            {
                const double decelerationTweaker = 0.3;
                double speed = dist / ((double)_deceleration * decelerationTweaker);
                speed = Math.Min(speed, MovingEntity.MaxSpeed);
                var desiredvelocity = toTarget * speed / dist;
                return (desiredvelocity - MovingEntity.Velocity);
            }
            return new Vector2D(0, 0);
        }

        public void SetDeceleration(Deceleration deceleration)
        {
            _deceleration = deceleration;
        }
    }
}