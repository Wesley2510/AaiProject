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

        public WanderBehaviour(Ant ant, Vector2D target) : base(ant)
        {
            Target = target;
        }

        public override Vector2D Calculate()
        {
            Vector2D circleCenter = Ant.Velocity.Clone();
            circleCenter.Normalize();
            return circleCenter;
        }
    }
}
