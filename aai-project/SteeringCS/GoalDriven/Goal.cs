using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.GoalDriven
{
    public abstract class Goal
    {
        public abstract void Activate();
        public abstract int Process();
        public abstract void Terminate();
        public abstract void AddSubGoal(Goal g);

    }
}
