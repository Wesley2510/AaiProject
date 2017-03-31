using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.behaviour
{
    public class HideBehaviour : SeekBehaviour
    {
        public HideBehaviour(MovingEntity movingEntity, Vector2D target) : base(movingEntity, target)
        {
        }
    }
}
