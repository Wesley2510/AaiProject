using AntSimulator.entity;
using AntSimulator.util;
using System.Collections.Generic;

namespace AntSimulator.behaviour
{
    public class ObstacleAvoidance : SteeringBehaviour
    {
        private enum pushWeight
        {
            LongDistance = 2,
            MediumDistance = 4,
            ShortDistance = 10
        }
        private pushWeight weighting;
        private float pushingForce;
        private float pushTweaker;
        private Vector2D vector1;
        private Vector2D vector2;
        private Vector2D vector3;

        public ObstacleAvoidance(MovingEntity movingentity) : base(movingentity)
        {
            pushingForce = 100f;
            pushTweaker = 0.35f;
        }

        public override Vector2D Calculate()
        {
            float dynamicLength = (float)(MovingEntity.Velocity.Length() / MovingEntity.MaxSpeed);
            vector1 = MovingEntity.Pos + Vector2D.Normalize(MovingEntity.Velocity) * dynamicLength;
            vector2 = MovingEntity.Pos + Vector2D.Normalize(MovingEntity.Velocity) * dynamicLength * 0.5f;
            vector3 = MovingEntity.Pos;
            List<Obstacle> potentialCollisions = MovingEntity.MyWorld.GetNearbyObstacles(vector1.Length(), MovingEntity.Pos);
            Obstacle mostThreatening = FindMostThreateningObstacle(potentialCollisions);
            Vector2D avoidence = new Vector2D();
            if (mostThreatening != null)
            {
                avoidence.X = vector1.X - mostThreatening.Pos.X;
                avoidence.Y = vector1.Y - mostThreatening.Pos.Y;
                avoidence = Vector2D.Normalize(avoidence);
                avoidence *= pushingForce;
            }
            else
            {
                avoidence *= 0;
            }
            avoidence *= (float)weighting;
            return avoidence * pushTweaker;
        }

        private Obstacle FindMostThreateningObstacle(List<Obstacle> potentialCollisions)
        {
            Obstacle mostThreatening = null;
            foreach (var obstacle in potentialCollisions)
            {
                bool collision = VectorInCircle(obstacle);
                if (collision && (mostThreatening == null || Vector2D.Distance(MovingEntity.Pos, obstacle.Pos) < Vector2D.Distance(MovingEntity.Pos, mostThreatening.Pos)))
                {
                    mostThreatening = obstacle;
                }
            }
            return mostThreatening;
        }

        private bool VectorInCircle(Obstacle obstacle)
        {
            if (Vector2D.Distance(obstacle.Pos, vector1) <= obstacle.Radius + MovingEntity.Radius)
            {
                weighting = pushWeight.LongDistance;
                return true;
            }
            if (Vector2D.Distance(obstacle.Pos, vector2) <= obstacle.Radius + MovingEntity.Radius)
            {
                weighting = pushWeight.MediumDistance;
                return true;
            }
            if (Vector2D.Distance(obstacle.Pos, vector3) <= obstacle.Radius + MovingEntity.Radius)
            {
                weighting = pushWeight.ShortDistance;
                return true;
            }
            return false;
        }
    }
}