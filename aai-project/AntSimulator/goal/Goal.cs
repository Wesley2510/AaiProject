using AntSimulator.entity;
using System.Collections.Generic;
using System.Drawing;

namespace AntSimulator.goal
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
        protected Ant Ant;
        protected Status Status;
        public Stack<Goal> Subgoals;

        protected Goal(Ant ant)
        {
            Ant = ant;
            Subgoals = new Stack<Goal>();
            Status = Status.Inactive;
        }

        public abstract void Activate();
        public abstract Status Process();
        public abstract void Terminate();
        public abstract void AddChild(Goal g);

        public bool IsActive()
        {
            return (Status == Status.Active);
        }

        public bool IsComplete()
        {
            return Status == Status.Completed;
        }

        public bool HasFailed()
        {
            return Status == Status.NotCompleted;
        }

        public bool IsInactive()
        {
            return Status == Status.Inactive;
        }

        public virtual void SetInactive()
        {
            Status = Status.Inactive;
        }

        public virtual PointF Render(Graphics g, int indentationX, PointF location)
        {
            PointF temp = location;
            temp.X += 5 * indentationX;
            location.Y += 10;
            SolidBrush drawBrush = new SolidBrush(Color.LawnGreen);
            Font font = new Font("Arial", 8);
            g.DrawString(GetType().Name, font, drawBrush, temp);
            Goal[] currentSubgoals = Subgoals.ToArray();
            foreach (Goal goal in currentSubgoals)
            {
                location = goal.Render(g, indentationX + 1, location);
            }
            return location;
        }


    }
}

