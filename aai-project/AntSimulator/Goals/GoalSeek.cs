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
    class GoalSeek : Goal
    {
        private Vector2D _target;
        private MovingEntity _me;
        private SteeringBehaviour _seekBehaviour;
        private int _distance;
        public GoalSeek(MovingEntity pMe, Vector2D pTarget, int pAllowedDistance) : base(pMe)
        {
            this._target = pTarget;
            _me = pMe;
            _distance =  pAllowedDistance;
            Activate();
        }

        public override void Activate()
        {
           _seekBehaviour = new Seek(_me, _target);
           _me.SteeringBehaviours.Add(_seekBehaviour);
            Process();
        }

        public override Status Process()
        {
            if (!isActive())
            {
                status = Status.Active;
                Activate();
            }
            
            if (Vector2D.Distance(_target, _me.Pos) < _distance)
            {
                status = Status.Completed;

            }
            return status;
        }

        public override void Terminate()
        {
            _me.SteeringBehaviours.Remove(_seekBehaviour);
        }

        public override void AddChild(Goal g)
        {
          
        }
    }
}
