using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntSimulator.entity;
using AntSimulator.util;

namespace AntSimulator.Goals
{
    class GoalGetFood : CompositeGoal
    {
        private Stopwatch time;

        public GoalGetFood(MovingEntity me) : base(me)
        {
            time = new Stopwatch();
        }

        public override void Activate()
        {


        }

        public override Vector2D Process()
        {
            throw new NotImplementedException();
        }

        public override void Terminate()
        {

        }
    }
}
