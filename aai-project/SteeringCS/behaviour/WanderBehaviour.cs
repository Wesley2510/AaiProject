using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.behaviour
{
    class WanderBehaviour : SteeringBehaviour
    {
        //ToDo  
        public Vector2D Target;

        public WanderBehaviour(MovingEntity movingEntity, Vector2D target) : base(movingEntity)
        {
            Target = target;
        }

        public override Vector2D Calculate()
        {
            Vector2D circleCenter = MovingEntity.Velocity.Clone();
            circleCenter.Normalize();
            return circleCenter;
        }
    }
}
