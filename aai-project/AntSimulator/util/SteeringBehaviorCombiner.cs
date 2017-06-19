using System.Collections.Generic;
using AntSimulator.behaviour;

namespace AntSimulator.util
{
    public class SteeringBehaviorCombiner
    {
        public static Vector2D CombineAllBehaviors(List<SteeringBehaviour> steeringBehaviours)
        {
            var steering = new Vector2D();

            foreach (var behavior in steeringBehaviours)
            {
                if(behavior.GetType() == typeof(Arrival))
                {
                    steering += behavior.Calculate();
                    continue;
                }
                if (behavior.GetType() == typeof(ObstacleAvoidance))
                {
                    steering += behavior.Calculate();
                    continue;
                }
                if (behavior.GetType() == typeof(ObstacleAvoidance))
                {
                    steering += behavior.Calculate() * 2;
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
