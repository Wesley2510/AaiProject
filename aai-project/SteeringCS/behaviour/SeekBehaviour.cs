using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
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
            Vector2D preNormalisedVelocity = Target - MovingEntity.Pos;
            Vector2D desiredVelocity = preNormalisedVelocity.Normalize() * MovingEntity.MaxSpeed;
            Vector2D steering = desiredVelocity - MovingEntity.Velocity;

            return steering;
        }       
    }
}
