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
        public Vector2D Target;
        public HideBehaviour(Ant ant, Vector2D target) : base(ant)
        {
            Target = target;
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
            throw new NotImplementedException();
        }
    }
}
