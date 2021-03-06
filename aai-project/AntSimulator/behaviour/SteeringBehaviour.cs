﻿using AntSimulator.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSimulator
{
    abstract class SteeringBehaviour
    {
        public MovingEntity ME { get; set; }
        public abstract Vector2D Calculate();

        public SteeringBehaviour(MovingEntity me)
        {
            ME = me;
        }
    }

    
}
