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
        public Stack<Goal> subgoals { get; protected set; }

        public CompositeGoal(MovingEntity me) : base(me)
        {
            subgoals = new Stack<Goal>();
        }

        public override void AddSubgoal(Goal g)
        {
            subgoals.Push(g);
            isCompleted = false;
        }

        public Vector2D ProcessSubgoals()
        {
            Vector2D returnValue = new Vector2D();

            if (subgoals.Count > 0)
            {
                returnValue += subgoals.Peek().Process();

                
                if (subgoals.Peek().isCompleted)
                {
                    subgoals.Peek().Terminate();
                    subgoals.Pop();
                }
            }
            else
            {
                isCompleted = true;
            }

            return returnValue;
        }

       public void RemoveAllSubgoals()
        {
            while (subgoals.Count > 0)
            {
                subgoals.Pop().Terminate();
            }
            subgoals.Clear();
        }

 public override string ToString()
        {
            string returnValue = "";
            if (subgoals.Count > 0)
            {
                returnValue += subgoals.Peek().ToString();
            }
            else
            {
                returnValue += this.GetType();
            }
            return returnValue;
        }
    }
}

