using AntSimulator.behaviour;
using AntSimulator.entity;
using AntSimulator.util;
using System;
using System.Collections.Generic;

namespace AntSimulator.goal
{
    class GoalGetFood : CompositeGoal
    {
        private Vector2D _target;
        protected List<Vector2D> Path;
        private Ant _ant;
        private SteeringBehaviour _seekBehaviour;
        private Food _closestFood;

        public GoalGetFood(Ant ant) : base(ant)
        {
            _ant = ant;
            List<Food> foods = ant.MyWorld.GetNearbyFood(500, ant.Pos);
            var distance = Int32.MaxValue;
            foreach (var food in foods)
            {
                if (Vector2D.Distance(ant.Pos, food.Pos) < distance)
                {
                    distance = (int)Vector2D.Distance(ant.Pos, food.Pos);
                    _closestFood = food;
                }
            }
            _target = _closestFood.Pos;
        }

        public override void Activate()
        {
            Ant.MyWorld.Graph.GetRoute(_ant.Pos, _target, out Path);
            Subgoals.Push(new GoalArrival(_ant, Path[0], 5));
            for (var index = 1; index < Path.Count; index++)
            {
                Vector2D vector2D = Path[index];
                Subgoals.Push(new GoalSeek(_ant, vector2D, 20));
            }
            _seekBehaviour = new Seek(_ant, _target);
            _ant.SteeringBehaviours.Add(_seekBehaviour);
            Process();
        }

        public override Status Process()
        {
            if (!IsActive())
            {
                Activate();
                Status = Status.Active;
            }
            ProcessSubGoals();
            if (Subgoals.Count != 0) return Status;
            Status = Status.Completed;

            return Status;
        }

        public override void Terminate()
        {

        }
    }
}
