using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntSimulator.entity;
using AntSimulator.util;

namespace AntSimulator.Goals
{
    public enum GoalStatus { Inactive, Active, Failed, Completed };
    public abstract class Goal
    {
        protected MovingEntity movingEntity { get; set; }
        public GoalStatus Status { get; set; }
        public bool isActive { get; set; }
        public bool isCompleted { get; set; }
        public bool hasFailed { get; set; }

        public Goal(MovingEntity me)
        {
            movingEntity = me;
        }

        public abstract void Activate();
        public abstract Vector2D Process();
        public abstract void Terminate();


        public virtual void AddSubgoal(Goal g) { }
    }


}

