using AntSimulator.entity;
using AntSimulator.util;
using System.Collections.Generic;

namespace AntSimulator.behaviour
{
    public class PathFollowing : SteeringBehaviour
    {
        private Vector2D _currentTarget;
        private Stack<Vector2D> _searchNodes;

        public PathFollowing(MovingEntity movingentity, Stack<Vector2D> searchNodes) : base(movingentity)
        {
            _searchNodes = searchNodes;
            _currentTarget = _searchNodes.Pop();
        }

        public override Vector2D Calculate()
        {
            if (Vector2D.Distance(MovingEntity.Pos, _currentTarget) <= 15)
                _currentTarget = _searchNodes.Pop();
            return _currentTarget != null ? new Seek(MovingEntity, _currentTarget).Calculate() : new Vector2D();
        }
    }
}
