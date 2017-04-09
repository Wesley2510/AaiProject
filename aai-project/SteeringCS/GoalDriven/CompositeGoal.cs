using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.GoalDriven
{
    class CompositeGoal : Goal
    {
        public List<Goal> SubGoals;


        public override void Activate()
        {
            throw new NotImplementedException();
        }

        public override int Process()
        {
            throw new NotImplementedException();
        }

        public override void Terminate()
        {
            throw new NotImplementedException();
        }

        public override void AddSubGoal(Goal g)
        {
            throw new NotImplementedException();
        }

        public int ProcessSubGoals()
        {
            throw new NotImplementedException();
        }
    }
}
