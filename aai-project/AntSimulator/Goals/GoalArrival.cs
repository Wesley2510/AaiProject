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
        private MovingEntity _me;

        public GoalArrival(MovingEntity pMe, Vector2D pTarget, int pDeviationallowance) : base(pMe)
        {
            this.target = pTarget;
            _me = pMe;
            this.deviationallowance = pDeviationallowance;
            Activate();
        }

        public override void Activate()
        {
            Vector2D Target = targetEntity != null ? targetEntity.Pos : target;
            attachBehaviour = new Arrival(_me, Target, Deceleration.Slow);
            _me.SteeringBehaviours.Add(attachBehaviour);
            //Process();
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
            if (Vector2D.Distance(Target, _me.Pos) < deviationallowance)
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
        }
    }
}
