using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.behaviour
{
    public class HideBehaviour : SteeringBehaviour
    {
        public Ant Target;
        public List<Dirt> Obstacles;
        public HideBehaviour(MovingEntity movingEntity, List<Dirt> obstacles, Ant target) : base(movingEntity)
        {
            Target = target;
            Obstacles = obstacles;
        }

        public Vector2D GetHidingPosition(Vector2D posObstacle, double radiusObstacle, Vector2D posTarget)
        {
            const double distanceFromBoundry = 30.0;
            double distanceAway = distanceFromBoundry + radiusObstacle;
            Vector2D toObstacle = Vector2D.Normalize(posObstacle- posTarget);
            return (toObstacle * distanceAway) + posObstacle;
        }

        public override Vector2D Calculate()
        {
            double distanceToClosest = double.MaxValue;
            Vector2D bestHidingSpot = new Vector2D();
            foreach (var obj in Obstacles)
            {
                Vector2D hidingSpot = GetHidingPosition(obj.Pos, obj.Scale, Target.Pos);
                double dist = Vector2D.DistanceSquared(hidingSpot, MovingEntity.Pos);
                if (dist < distanceToClosest)
                {
                    distanceToClosest = dist;
                    bestHidingSpot = hidingSpot;
                }
            }

            if (distanceToClosest == double.MaxValue)
            {
                new FleeBehavior(MovingEntity, bestHidingSpot);
            }
            else
            {
                new ArrivalBehavior(MovingEntity, bestHidingSpot, Deceleration.Fast);
            }
            return bestHidingSpot;
            
        }
    }
}
