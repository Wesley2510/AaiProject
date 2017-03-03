using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.util;

namespace SteeringCS.behaviour
{
    public class SeekBehaviour
    {
        private readonly MovingEntity _movingEntity;
        private readonly Vehicle _vehicle;        

        public Vector2D Seek(Vector2D target)
        {
            Vector2D desiredVelocity = (target.Sub(_movingEntity.Pos)).Multiply(_vehicle.MaxSpeed);
            return desiredVelocity.Sub(_vehicle.Velocity);
        }
    }
}
