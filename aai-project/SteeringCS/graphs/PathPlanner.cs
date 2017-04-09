using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.util;

namespace SteeringCS.graphs
{
    public class PathPlanner
    {
        private Graph _graph = new Graph();
        private Ant _ant;
        public Vector2D Destination;

        public PathPlanner(Ant ant)
        {
            _ant = ant;
        }

        private int GetClosestNodeToPosition(Vector2D position)
        {
            throw new NotImplementedException();
        }

        public bool CreatePathToPosition(Vector2D target, List<Vector2D> path)
        {
            Destination = target;

            throw new NotImplementedException();
        }

    }
}
