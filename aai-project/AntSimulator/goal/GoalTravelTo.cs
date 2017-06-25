using AntSimulator.entity;
using System;

namespace AntSimulator.goal
{
    class GoalTravelTo : CompositeGoal
    {
        public GoalTravelTo(Ant ant) : base(ant)
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
