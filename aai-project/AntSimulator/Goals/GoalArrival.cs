using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntSimulator.behaviour;
using AntSimulator.entity;
using AntSimulator.util;

namespace AntSimulator.Goals
{
    class GoalArrival : Goal
    {

        private Vector2D target;
        private BaseGameEntity targetEntity;
        private Arrival attachBehaviour;
        private int deviationallowance;

      
        public GoalArrival(MovingEntity pMe, Vector2D pTarget, int pDeviationallowance) : base(pMe)
        {
            this.target = pTarget;
            this.deviationallowance = pDeviationallowance;
        }

        public override void Activate()
        {
            Vector2D Target = targetEntity != null ? targetEntity.Pos : target;
            attachBehaviour = new Arrival(me, Target, Deceleration.Slow);
            me.SteeringBehaviours.Add(attachBehaviour);

        }

        public override Status Process()
        {
            if (!isActive())
            {
                Activate();
                status = Status.Active;
            }
            Vector2D Target = targetEntity != null ? targetEntity.Pos : target;
            attachBehaviour.Target = Target;
            if (Vector2D.Distance(Target, me.Pos) < deviationallowance)
            {
                status = Status.Completed;
            }
            return status;
        }

        public override void Terminate()
        {
            me.SteeringBehaviours.Remove(attachBehaviour);
        }

        public override void AddChild(Goal g)
        {
            throw new NotImplementedException();
        }
    }
}
