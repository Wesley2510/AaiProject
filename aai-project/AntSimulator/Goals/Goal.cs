using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntSimulator.entity;
using AntSimulator.util;

namespace AntSimulator.Goals
{
    public enum Status
    {
        Inactive = 0,
        Active = 1,
        Completed = 2,
        NotCompleted = 3
    }

    public abstract class Goal
    {
        protected MovingEntity me;
        protected Status status;
        private bool isComplex;
        public Stack<Goal> Subgoals;

        protected Goal(MovingEntity me)
        {
            this.me = me;
            Subgoals = new Stack<Goal>();
            status = Status.Inactive;
        }

        public abstract void Activate();
        public abstract Status Process();
        public abstract void Terminate();
        public abstract void AddChild(Goal g);

        public bool isActive()
        {
            return (status == Status.Active);
        }

        public bool isComplete()
        {
            return status == Status.Completed;
        }

        public bool hasFailed()
        {
            return status == Status.NotCompleted;
        }

        public bool isInactive()
        {
            return status == Status.Inactive;
        }

        public virtual void SetInactive()
        {
            status = Status.Inactive;
        }

        public virtual PointF Render(Graphics g, int indentationX, PointF location)
        {
            PointF temp = location;
            temp.X += 5 * indentationX;
            location.Y += 10;
            SolidBrush drawBrush = new SolidBrush(Color.LawnGreen);
            Font font = new Font("Arial", 8);
            g.DrawString(this.GetType().Name, font, drawBrush, temp);
            Goal[] currentSubgoals = Subgoals.ToArray();
            foreach (Goal goal in currentSubgoals)
            {
                location = goal.Render(g, indentationX + 1, location);
            }
            return location;
        }


    }
}

