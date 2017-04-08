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
        public Vector2D Target;

        public ArrivalBehavior(Ant ant, Vector2D target, Deceleration deceleration) : base(ant)
        {
            Target = target;
            Deceleration = deceleration;
        }

        public override Vector2D Calculate()
        {
            Vector2D toTarget = Target - Ant.Pos;
            double distance = Target.Length();

            if (distance > 0)
            {               
                const double decelerationTweaker = 0.3;
                double speed = distance / ((double) Deceleration * decelerationTweaker);
                speed = Math.Min(speed, Ant.MaxSpeed);
                Vector2D desiredVelocity = toTarget * speed / distance;
                return (desiredVelocity - Ant.Velocity);
            }
            return new Vector2D(0,0);
        }
    }
}