using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntSimulator.entity;
using AntSimulator.GraphPath;
using AntSimulator.util;

namespace AntSimulator.Goals
{
    class GoalFollowPath : CompositeGoal
    {
        public List<Node> Path { get; set; }
        private Vector2D _to;

        // With algoritme
        private float _startTime { get; set; }

        // Name of a gameobject
        private string _element;

        // If calculation is needed
        private bool _calc;
        public GoalFollowPath(MovingEntity me, List<Node> pPath) : base(me)
        {
            Path = pPath;
            _calc = false;
        }

        public override void Activate()
        {
            throw new NotImplementedException();
        }

        public override Vector2D Process()
        {
            throw new NotImplementedException();
        }

        public override void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
