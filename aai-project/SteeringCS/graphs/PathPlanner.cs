using SteeringCS.entity;
using SteeringCS.util;
using SteeringCS.world;

namespace SteeringCS.graphs
{
    public class PathPlanner
    {
        private Graph _graph;
        private MovingEntity _movingEntity;
        public Vector2D Destination;

        public PathPlanner(MovingEntity movingEntity, World w)
        {
            _movingEntity = movingEntity;
            _graph = w.Graph;
        }

    }
}
