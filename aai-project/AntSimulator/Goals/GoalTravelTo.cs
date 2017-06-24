using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntSimulator.entity;
using AntSimulator.util;

namespace AntSimulator.Goals
{
    class GoalTravelTo : CompositeGoal
    {
        public GoalTravelTo(MovingEntity me) : base(me)
        {
        }

        public override void Activate()
        {
            //arrival
            //seek
           
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            throw new NotImplementedException();
        }

        public override void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
