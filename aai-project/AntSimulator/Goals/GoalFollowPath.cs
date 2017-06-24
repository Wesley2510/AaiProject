using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntSimulator.behaviour;
using AntSimulator.entity;
using AntSimulator.graph;
using AntSimulator.GraphPath;
using AntSimulator.util;

namespace AntSimulator.Goals
{
    class GoalFollowPath : CompositeGoal
    {

        public  List<Vector2D> _path;
        public Vector2D target;


        public GoalFollowPath(MovingEntity me, Vector2D pTarget) : base(me)
        {
          
            target = pTarget;
          
        }

        public override void Activate()
        {
            List<Vector2D> _path = new List<Vector2D>();
            _path =  me.MyWorld._graph.getRoute(me.Pos, target);
            Subgoals.Push(new GoalArrival(me, _path[0], 5));
            for (var index = 1; index < _path.Count; index++)
            {
                Vector2D vector2D = _path[index];
                Subgoals.Push(new GoalSeek(me, vector2D, 20));
            }
        }

        public override Status Process()
        {
            
            if (!isActive())
            {
                Activate();
                status = Status.Active;
            }
            ProcessSubGoals();
            if (Subgoals.Count != 0) return status;
            status = Status.Completed;

            return status;
        }

        public override void Terminate()
        {
            throw new NotImplementedException();
        }
    }

}
