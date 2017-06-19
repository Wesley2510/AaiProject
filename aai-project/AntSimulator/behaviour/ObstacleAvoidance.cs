using AntSimulator.entity;
using AntSimulator.util;
using System.Collections.Generic;

namespace AntSimulator.behaviour
{
    public class ObstacleAvoidance : SteeringBehaviour
    {
        private float MaxSeeAhead;
        private float MaxPushingForce;

        private enum pushWeight
        {
            LongDistance = 2,
            MediumDistance = 4,
            ShortDistance = 10
        }

        private pushWeight weighting;
        private float pushtweaker;
        private Vector2D ahead1;
        private Vector2D ahead2;
        private Vector2D ahead3;

        public ObstacleAvoidance(MovingEntity movingentity) : base(movingentity)
        {
            MaxSeeAhead = 100f;
            MaxPushingForce = 100f;
            pushtweaker = 0.35f;
        }

        public override Vector2D Calculate()
        {
            float dynamic_length = (float)(MovingEntity.Velocity.Length() / MovingEntity.MaxSpeed);
            ahead1 = MovingEntity.Pos + Vector2D.Normalize(MovingEntity.Velocity) * dynamic_length;
            ahead2 = MovingEntity.Pos + Vector2D.Normalize(MovingEntity.Velocity) * dynamic_length * 0.5f;
            ahead3 = MovingEntity.Pos;
            List<Obstacle> potentialCollisions =
                MovingEntity.MyWorld.getNearbyObstacles(ahead2.Length(), MovingEntity.Pos);
            Obstacle mostThreatening = FindMostThreateningObstacle(potentialCollisions);
            Vector2D avoidence = new Vector2D();
            if (mostThreatening != null)
            {
                avoidence = new Vector2D(
                    ahead1.X - mostThreatening.Pos.X, ahead1.Y - mostThreatening.Pos.Y);
                avoidence = Vector2D.Normalize(avoidence);
                avoidence *= MaxPushingForce;
            }
            else
            {
                avoidence *= 0;
            }
            avoidence *= (float)(weighting);
            return avoidence * pushtweaker;
        }

        private Obstacle FindMostThreateningObstacle(List<Obstacle> potentialCollisions)
        {
            Obstacle mostThreatening = null;
            foreach (var obstacle in potentialCollisions)
            {
                bool collision = VectorInCircle(obstacle);
                if (collision && mostThreatening == null ||
                    Vector2D.Distance(MovingEntity.Pos, obstacle.Pos) <
                    Vector2D.Distance(MovingEntity.Pos, mostThreatening.Pos))
                {
                    mostThreatening = obstacle;
                }
            }
            return mostThreatening;
        }

        private bool VectorInCircle(Obstacle obstacle)
        {
            if (Vector2D.Distance(obstacle.Pos, ahead1) <= obstacle.Radius + MovingEntity.Radius)
            {
                weighting = pushWeight.LongDistance;
                return true;
            }
            if (Vector2D.Distance(obstacle.Pos, ahead2) <= obstacle.Radius + MovingEntity.Radius)
            {
                weighting = pushWeight.MediumDistance;
                return true;
            }
            if (Vector2D.Distance(obstacle.Pos, ahead3) <= obstacle.Radius + MovingEntity.Radius)
            {
                weighting = pushWeight.ShortDistance;
                return true;
            }
            return false;
        }
    }
}