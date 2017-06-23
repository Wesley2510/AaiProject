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

        public GoalArrival(MovingEntity me) : base(me)
        {
        }
        public GoalArrival(MovingEntity pMe, Vector2D pTarget, int pDeviationallowance) : base(pMe)
        {
            this.target = pTarget;
            this.deviationallowance = pDeviationallowance;
        }

        public override void Activate()
        {
            Vector2D Target = targetEntity != null ? targetEntity.Pos : target;
            attachBehaviour = new Arrival(movingEntity, Target, Deceleration.Slow);
            movingEntity.SteeringBehaviours.Add(attachBehaviour);

        }

        public override Vector2D Process()
        {
            if (!isActive)
            {
                Activate();
                Status = GoalStatus.Active;
            }
            Vector2D Target = targetEntity != null ? targetEntity.Pos : target;
            attachBehaviour.Target = Target;
            if (Vector2D.Distance(Target, movingEntity.Pos) < deviationallowance)
            {
                Status = GoalStatus.Completed;
            }
            return new Vector2D();
        }

        public override void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
