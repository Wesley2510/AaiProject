using AntSimulator.entity;
using AntSimulator.util;
using System;
using System.Collections.Generic;

namespace AntSimulator.behaviour
{
    public class Explore : SteeringBehaviour
    {
        private BaseGameEntity _searchTarget;
        private Vector2D _areaStart;
        private Vector2D _areaEnd;
        private float _searchSize;
        private PathFollowing _pathFollowing;

        public Explore(MovingEntity movingentity, BaseGameEntity searchTarget, Vector2D areaStart, Vector2D areaEnd, float searchSize) : base(movingentity)
        {
            _searchTarget = searchTarget;
            _areaStart = areaStart;
            _areaEnd = areaEnd;
            _searchSize = searchSize;
            _pathFollowing = new PathFollowing(MovingEntity, SearchNodes());
        }

        private Stack<Vector2D> SearchNodes()
        {
            var searchNodes = new Stack<Vector2D>();
            var searchWidth = Vector2D.Distance(_areaStart, new Vector2D(_areaEnd.X, _areaStart.Y));
            var searchHeight = Vector2D.Distance(_areaEnd, new Vector2D(_areaEnd.Y, _areaStart.X));
            var collumns = (float)Math.Ceiling(searchWidth / _searchSize);
            var rows = (float)Math.Ceiling(searchHeight / _searchSize);
            for (var i = 0; i < rows; i++)
            {
                if (i % 2 == 0)
                    for (int j = 0; j < collumns; j++)
                        searchNodes.Push(new Vector2D(_areaStart.X + j * _searchSize, _areaStart.Y + i * _searchSize));
                else
                    for (int j = (int)(collumns - 1); j >= 0; j++)
                        searchNodes.Push(new Vector2D(_areaStart.X + j * _searchSize, _areaStart.Y + i * _searchSize));
            }
            return searchNodes;
        }

        public override Vector2D Calculate()
        {
            if (Vector2D.Distance(MovingEntity.Pos, _searchTarget.Pos) - _searchTarget.Radius - MovingEntity.Radius <= _searchSize)
                return new Arrival(MovingEntity, _searchTarget.Pos, Deceleration.Fast).Calculate();
            return _pathFollowing.Calculate();
        }
    }
}
