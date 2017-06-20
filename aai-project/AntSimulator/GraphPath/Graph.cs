using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Security.Policy;
using AntSimulator.entity;
using AntSimulator.util;
using AntSimulator.world;

namespace AntSimulator.GraphPath
{
    class Graph
    {
        private Dictionary<string, Node> _nodeMap = new Dictionary<string, Node>();
        private World _world;
        private int _nodeDistance = 15;
        private float _maxRadius;
        
        public Node startnode;

        public Graph(World pWorld)
        {
            _world = pWorld;
            startnode = new Node(20, 20);
            _maxRadius = 0;
            foreach (Obstacle obstacle in _world.Obstacles)
            {
                _maxRadius = (_maxRadius < obstacle.Radius ? obstacle.Radius : _maxRadius);
            }
          
        }

        
        private void addEdge(string name1, string name2, float cost)
        {
            Node v = _nodeMap[name1];
            Node w = _nodeMap[name2];
            foreach (Edge edge in v.adjEdges)
            {
                if (edge.dest == w)
                {
                    return;
                }
            }
            v.adjEdges.Add(new Edge(w, cost));
            w.adjEdges.Add(new Edge(v, cost));
        }

        private void ClearAll()
        {
            foreach (KeyValuePair<string, Node> keyValuePair in _nodeMap)
            {
                keyValuePair.Value.Reset();
            }
        }

        private void topEdges(Node node)
        {
            float x = (float)node.position.X;
            float y = (float)node.position.Y;
            Node tl = new Node(x + _nodeDistance, y - _nodeDistance);
            Node top = new Node(x, y - _nodeDistance);
            Node tr = new Node(x + _nodeDistance, y - _nodeDistance);
            if (_nodeMap.ContainsKey(tl.id))
            {
                addEdge(node.id, tl.id, (float)Math.Sqrt(2));
            }
            if (_nodeMap.ContainsKey(tr.id))
            {
                addEdge(node.id, tr.id, 1);
            }
            if (_nodeMap.ContainsKey(top.id))
            {
                addEdge(node.id, top.id, (float)Math.Sqrt(2));
            }
        }

        private void leftEdge(Node node)
        {
            float x = (float)node.position.X;
            float y = (float)node.position.Y;
            Node left = new Node(x - _nodeDistance, y);
            if (_nodeMap.ContainsKey(left.id))
            {
                addEdge(node.id, left.id, 1);
            }
        }

        private void rightEdge(Node node)
        {
            float x = (float)node.position.X;
            float y = (float)node.position.Y;
            Node right = new Node(x + _nodeDistance, y);
            if (_nodeMap.ContainsKey(right.id))
            {
                addEdge(node.id, right.id, 1);
            }
        }

        private void botEdges(Node node)
        {
            float x = (float)node.position.X;
            float y = (float)node.position.Y;
            Node bl = new Node(x + _nodeDistance, y + _nodeDistance);
            Node bot = new Node(x, y + _nodeDistance);
            Node br = new Node(x + _nodeDistance, y + _nodeDistance);
            if (_nodeMap.ContainsKey(bl.id))
            {
                addEdge(node.id, bl.id, (float)Math.Sqrt(2));
            }
            if (_nodeMap.ContainsKey(bot.id))
            {
                addEdge(node.id, bot.id, 1);
            }
            if (_nodeMap.ContainsKey(br.id))
            {
                addEdge(node.id, br.id, (float)Math.Sqrt(2));
            }
        }

        private void Edges(Node node)
        {
            topEdges(node);
            leftEdge(node);
            rightEdge(node);
            botEdges(node);
        }
        public void FloodFill(Node current)
        {
            float x = (float)current.position.X;
            float y = (float)current.position.Y;
            Node top = new Node(x, y - _nodeDistance);
            Node left = new Node(x - _nodeDistance, y);
            Node right = new Node(x + _nodeDistance, y);
            Node bottom = new Node(x, y + _nodeDistance);

            if (checkNode(bottom))
            {
                _nodeMap.Add(bottom.id, bottom);

                FloodFill(bottom);
            }
            if (checkNode(top))
            {
                _nodeMap.Add(top.id, top);

                FloodFill(top);
            }
            if (checkNode(left))
            {
                _nodeMap.Add(left.id, left);
                FloodFill(left);
            }
            if (checkNode(right))
            {
                _nodeMap.Add(right.id, right);
                FloodFill(right);
            }
            Edges(current);
        }


        private bool checkNode(Node node)
        {
            var x = node.position.X;
            var y = node.position.Y;
            List<Obstacle> obstacles = _world.Obstacles;// GetNearbyObstacles(_maxRadius + 5, node.position);
            foreach (Obstacle obstacle in obstacles)
            {
                if (Vector2D.Distance(node.position,new Vector2D(obstacle.Pos.X + (obstacle.size/2),obstacle.Pos.Y+ (obstacle.size / 2)))  < obstacle.size/2 + 10)
                {
                    return false;
                }
            }
            if (x < 0 || y < 0 || x > _world.Width || y > _world.Height || _nodeMap.ContainsKey(node.id))
            {
                return false;
            }

            return true;
        }
        public void Render(Graphics G)
        {
            Pen nodepen = new Pen(Color.Blue, 2f);
            Pen EdgeTested = new Pen(Color.Purple, 2f);
            Pen EdgePen = new Pen(Color.LightGray, 1f);
            foreach (KeyValuePair<string, Node> n in _nodeMap)
            {
                foreach (Edge e in n.Value.adjEdges)
                {
                    if (n.Value.scratch == 0)
                    {
                        //G.DrawLine(EdgePen, n.Value.position.X, n.Value.position.Y, e.dest.position.X, e.dest.position.Y);
                        G.DrawLine(EdgePen,(float)n.Value.position.X, (float)n.Value.position.Y, (float)e.dest.position.X, (float)e.dest.position.Y);
                    }
                    else
                    {
                        G.DrawLine(EdgeTested, (float)n.Value.position.X, (float)n.Value.position.Y, (float)e.dest.position.X,
                            (float)e.dest.position.Y);
                    }
                }
                G.DrawEllipse(nodepen, (float)n.Value.position.X - 2, (float)n.Value.position.Y - 2, 4, 4);
            }
        }
    }
}
