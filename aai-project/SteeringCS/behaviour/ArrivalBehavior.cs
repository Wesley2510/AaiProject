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
            Vector2D desiredVelocity = Target - MovingEntity.Pos;
            double distance = Vector2D.Length(desiredVelocity);


            throw new NotImplementedException();
        }
    }
}
