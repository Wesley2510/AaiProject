using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntSimulator.behaviour;
using AntSimulator.entity;
using AntSimulator.util;
using AntSimulator.world;

namespace AntSimulator.Goals
{
    class GoalGetFood : CompositeGoal
    {
        private Vector2D _target;
        private MovingEntity _me;
        private SteeringBehaviour _seekBehaviour;
        private int _distance;
        private Food closestFood;
        
        public GoalGetFood(MovingEntity me) : base(me)
        {
            _me = me;
           List<Food> foods = me.MyWorld.GetNearbyFood(500, me.Pos);
            _distance = Int32.MaxValue;
            foreach (var food in foods)
            {
                if (Vector2D.Distance(me.Pos, food.Pos) < _distance)
                {
                    _distance = (int) Vector2D.Distance(me.Pos, food.Pos);
                    closestFood = food;
                }
            }
            _target = closestFood.Pos;
        }

        public override void Activate()
        {
            List<Vector2D> _path = new List<Vector2D>();
            _path = _me.MyWorld._graph.getRoute(_me.Pos, _target);

            Subgoals.Push(new GoalArrival(_me ,_path[0], 5));
            for (var index = 1; index < _path.Count; index++)
            {
                Vector2D vector2D = _path[index];
                Subgoals.Push(new GoalSeek(_me, vector2D, 20));
            }

            //_seekBehaviour = new Seek(_me, _target);
            //_me.SteeringBehaviours.Add(_seekBehaviour);
            // Process();
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

        }
    }
}
