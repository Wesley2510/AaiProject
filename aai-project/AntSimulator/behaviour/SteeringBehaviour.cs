using System.Collections.Generic;
using AntSimulator.entity;
using AntSimulator.util;

namespace AntSimulator.behaviour
{
    public abstract class SteeringBehaviour
    {
        public MovingEntity MovingEntity { get; set; }
        public Vector2D Target { get; set; }
        public abstract Vector2D Calculate();

        protected SteeringBehaviour(MovingEntity movingentity)
        {
            MovingEntity = movingentity;
        }

        public static Vector2D CombineAllBehaviors(List<SteeringBehaviour> steeringBehaviours)
        {
            var steering = new Vector2D();

            foreach (var behavior in steeringBehaviours)
            {
                if (behavior.GetType() == typeof(Arrival))
                {
                    steering += behavior.Calculate();
                    continue;
                }
                if (behavior.GetType() == typeof(ObstacleAvoidance))
                {
                    steering += behavior.Calculate();
                    continue;
                }
                if (behavior.GetType() == typeof(Seek))
                {
                    steering += behavior.Calculate();
                }
            }
            return steering;
        }

    }
}
