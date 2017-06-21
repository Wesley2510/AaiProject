using System;
using System.Collections.Generic;
using System.Drawing;
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
        
        public Node startigNode;

        public Graph(World pWorld)
        {
            _world = pWorld;
            startigNode = new Node(20, 20);
            _maxRadius = 0;
            foreach (Obstacle obstacle in _world.Obstacles)
            {
                _maxRadius = (_maxRadius < obstacle.Radius ? obstacle.Radius : _maxRadius);
            }
          
        }

        
        private void addEdge(string pName1, string pName2, float pCost)
        {
            Node v = _nodeMap[pName1];
            Node w = _nodeMap[pName2];
            foreach (Edge edge in v.adjEdges)
            {
                if (edge.destination == w)
                {
                    return;
                }
            }
            v.adjEdges.Add(new Edge(w, pCost));
            w.adjEdges.Add(new Edge(v, pCost));
        }

       
        private void topEdge(Node pNode)
        {
            var x = (float)pNode.position.X;
            var y = (float)pNode.position.Y;
            Node tl = new Node(x + _nodeDistance, y - _nodeDistance);
            Node top = new Node(x, y - _nodeDistance);
            Node tr = new Node(x + _nodeDistance, y - _nodeDistance);
            if (_nodeMap.ContainsKey(tl.id))
            {
                addEdge(pNode.id, tl.id, (float)Math.Sqrt(2));
            }
            if (_nodeMap.ContainsKey(tr.id))
            {
                addEdge(pNode.id, tr.id, 1);
            }
            if (_nodeMap.ContainsKey(top.id))
            {
                addEdge(pNode.id, top.id, (float)Math.Sqrt(2));
            }
        }

        private void leftEdge(Node pNode)
        {
            var x = (float)pNode.position.X;
            var y = (float)pNode.position.Y;
            Node left = new Node(x - _nodeDistance, y);
            if (_nodeMap.ContainsKey(left.id))
            {
                addEdge(pNode.id, left.id, 1);
            }
        }

        private void rightEdge(Node pNode)
        {
            var x = (float)pNode.position.X;
            var y = (float)pNode.position.Y;
            Node right = new Node(x + _nodeDistance, y);
            if (_nodeMap.ContainsKey(right.id))
            {
                addEdge(pNode.id, right.id, 1);
            }
        }

        private void botEdges(Node pNode)
        {
            var x = (float)pNode.position.X;
            var y = (float)pNode.position.Y;
            Node bl = new Node(x + _nodeDistance, y + _nodeDistance);
            Node bot = new Node(x, y + _nodeDistance);
            Node br = new Node(x + _nodeDistance, y + _nodeDistance);
            if (_nodeMap.ContainsKey(bl.id))
            {
                addEdge(pNode.id, bl.id, (float)Math.Sqrt(2));
            }
            if (_nodeMap.ContainsKey(bot.id))
            {
                addEdge(pNode.id, bot.id, 1);
            }
            if (_nodeMap.ContainsKey(br.id))
            {
                addEdge(pNode.id, br.id, (float)Math.Sqrt(2));
            }
        }

        private void Edges(Node pNode)
        {
            topEdge(pNode);
            leftEdge(pNode);
            rightEdge(pNode);
            botEdges(pNode);
        }
        public void generateGraph(Node pCurrent)
        {
            var x = (float)pCurrent.position.X;
            var y = (float)pCurrent.position.Y;
            Node top = new Node(x, y - _nodeDistance);
            Node left = new Node(x - _nodeDistance, y);
            Node right = new Node(x + _nodeDistance, y);
            Node bottom = new Node(x, y + _nodeDistance);

            if (checkNode(bottom))
            {
                _nodeMap.Add(bottom.id, bottom);

                generateGraph(bottom);
            }
            if (checkNode(top))
            {
                _nodeMap.Add(top.id, top);

                generateGraph(top);
            }
            if (checkNode(left))
            {
                _nodeMap.Add(left.id, left);
                generateGraph(left);
            }
            if (checkNode(right))
            {
                _nodeMap.Add(right.id, right);
                generateGraph(right);
            }
            Edges(pCurrent);
        }


        private bool checkNode(Node pNode)
        {
            var x = pNode.position.X;
            var y = pNode.position.Y;
            List<Obstacle> obstacles = _world.Obstacles;
            foreach (Obstacle obstacle in obstacles)
            {
                if (Vector2D.Distance(pNode.position,new Vector2D(obstacle.Pos.X + (obstacle.size/2),obstacle.Pos.Y+ (obstacle.size / 2)))  < obstacle.size/2 + 10)
                {
                    return false;
                }
            }
            if (x < 0 || y < 0 || x > _world.Width || y > _world.Height || _nodeMap.ContainsKey(pNode.id))
            {
                return false;
            }

            return true;
        }
        public void Render(Graphics G)
        {
            Pen nodepen = new Pen(Color.Blue, 2f);
            Pen EdgeTested = new Pen(Color.Green, 2f);
            Pen EdgePen = new Pen(Color.LightGray, 1f);
            foreach (KeyValuePair<string, Node> n in _nodeMap)
            {
                foreach (Edge e in n.Value.adjEdges)
                {
                    if (n.Value.scratch == 0)
                    {
                        G.DrawLine(EdgePen,(float)n.Value.position.X, (float)n.Value.position.Y, (float)e.destination.position.X, (float)e.destination.position.Y);
                    }
                    else
                    {
                        G.DrawLine(EdgeTested, (float)n.Value.position.X, (float)n.Value.position.Y, (float)e.destination.position.X,
                            (float)e.destination.position.Y);
                    }
                }
                G.DrawEllipse(nodepen, (float)n.Value.position.X - 2, (float)n.Value.position.Y - 2, 4, 4);
            }
        }
    }
}
