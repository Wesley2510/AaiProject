using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.behaviour
{
    public class EvadeBehavior
    {
        public Ant Pursuer;

        public EvadeBehavior(Ant pursuer, MovingEntity movingEntity)
        {
            Pursuer = pursuer;
            Vector2D toPursuer = pursuer.Pos - movingEntity.Pos;
            double lookAheadTime = toPursuer.Length() / (movingEntity.MaxSpeed + pursuer.MaxSpeed);
            new FleeBehavior(movingEntity, pursuer.Pos + pursuer.Velocity * lookAheadTime);
        }
    }
}
