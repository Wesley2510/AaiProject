using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;
using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.State
{
    public abstract class State
    {

        public abstract void Enter(MovingEntity movingEntity);
        public abstract void Execute(MovingEntity movingEntity);
        public abstract void Exit(MovingEntity movingEntity);
    }

}
