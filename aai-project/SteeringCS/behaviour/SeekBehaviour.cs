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
    public class SeekBehaviour
    {
        private readonly MovingEntity _entity;             

        public Vector2D Seek(Vector2D target)
        {
            _entity.Steering = _entity.Steering.Truncate(_entity.MaxSpeed);
            _entity.Steering = _entity.Steering / _entity.Mass;

            _entity.Velocity = (_entity.Velocity + _entity.Steering).Truncate(_entity.MaxSpeed);
            _entity.Pos = _entity.Pos + target;

            return _entity.Pos;
        }
    }
}
