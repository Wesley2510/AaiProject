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
        public Vehicle Pursuer;

        public EvadeBehavior(Vehicle pursuer, Ant ant)
        {
            Pursuer = pursuer;
            Vector2D toPursuer = pursuer.Pos - ant.Pos;
            double lookAheadTime = toPursuer.Length() / (ant.MaxSpeed + pursuer.MaxSpeed);
            new FleeBehavior(ant, pursuer.Pos + pursuer.Velocity * lookAheadTime);
        }
    }
}
