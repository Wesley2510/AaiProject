using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntSimulator.entity;
using AntSimulator.graph;
using AntSimulator.GraphPath;
using AntSimulator.util;

namespace AntSimulator.Goals
{
    class GoalFollowPath : CompositeGoal
    {
        public List<Node> Path { get; set; }
        public Vector2D target;
        private Vector2D _to;

        // With algoritme
        private float _startTime { get; set; }

        // Name of a gameobject
        private string _element;

        // If calculation is needed
        private bool _calc;
        public GoalFollowPath(MovingEntity me, Vector2D pTarget) : base(me)
        {
            target = pTarget;
        }

        public override void Activate()
        {
            //movingEntity.MyWorld._graph(movingEntity.Pos, target, Path);
            //subgoals.Push(new goalArrival(movingEntity, Path[0], 5));
            //for (var index = 1; index < Path.Count; index++)
            //{
            //    Vector2D vector2D = Path[index];
            //    subgoals.Push(new GoalSeek(movingEntity, vector2D, 20));
            //}
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
