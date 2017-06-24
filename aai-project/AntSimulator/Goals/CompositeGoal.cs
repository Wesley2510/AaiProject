using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntSimulator.entity;
using AntSimulator.util;

namespace AntSimulator.Goals
{
    public abstract class CompositeGoal : Goal
    {
        protected CompositeGoal(MovingEntity me) : base(me)
        {
        }

        public override void AddChild(Goal g)
        {
            Subgoals.Push(g);
        }

        public override void Terminate()
        {
            foreach (Goal g in Subgoals)
            {
                g.Terminate();
            }
        }

        public Status ProcessSubGoals()
        {
            while (Subgoals.Count != 0 && (Subgoals.Peek().isComplete() || Subgoals.Peek().hasFailed()))
            {
                Subgoals.Peek().Terminate();
                Subgoals.Pop();
            }
            if (Subgoals.Count > 0)
            {
                Status StatusOfSubGoals = Subgoals.Peek().Process();
                if (StatusOfSubGoals == Status.Completed && Subgoals.Count > 1)
                {
                    return Status.Active;
                }
                return StatusOfSubGoals;
            }
            return Status.Completed;
        }


        public override void SetInactive()
        {
            Terminate();
            base.SetInactive();
        }
    }
}