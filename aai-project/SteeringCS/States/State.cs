using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;
using SteeringCS.entity;
using SteeringCS.graphs;
using SteeringCS.util;

namespace SteeringCS.States
{
    public abstract class State
    {

        public abstract void Enter(MovingEntity movingEntity, Node food);
        public abstract void Execute(MovingEntity movingEntity);
        public abstract void Exit(MovingEntity movingEntity);

    }

}
