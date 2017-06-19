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
        private Dictionary<string, Node> nodeMap = new Dictionary<string, Node>();
        private World world;
     private int nodeDistance = 15;
        private float maxRadius;
        public bool IsBusy;

        public Node startnode;

        public Graph(World pWorld)
        {
            world = pWorld;
            startnode = new Node(20, 20);
            maxRadius = 0;
            foreach (Obstacle obstacle in world.Obstacles)
            {
                maxRadius = (maxRadius < obstacle.Radius ? obstacle.Radius : maxRadius);
            }
            IsBusy = false;
        }

        private void addNode(int x, int y)
        {
            Node v = new Node(x, y);
            nodeMap.Add(v.id, v);
        }

        private void addEdge(string name1, string name2, float cost)
        {
            Node v = nodeMap[name1];
            Node w = nodeMap[name2];
            foreach (Edge edge in v.adjEdges)
            {
                if (edge == new Edge(w, cost))
                {
                    return;
                }
            }
            v.adjEdges.Add(new Edge(w, cost));
        }

        private void addTwoWayEdge(string name1, string name2, float cost)
        {
            Node v = nodeMap[name1];
            Node w = nodeMap[name2];
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
            foreach (KeyValuePair<string, Node> keyValuePair in nodeMap)
            {
                keyValuePair.Value.Reset();
            }
        }

        private void topEdges(Node node)
        {
            float x = (float)node.position.X;
            float y = (float)node.position.Y;
            Node tl = new Node(x + nodeDistance, y - nodeDistance);
            Node top = new Node(x, y - nodeDistance);
            Node tr = new Node(x + nodeDistance, y - nodeDistance);
            if (nodeMap.ContainsKey(tl.id))
            {
                this.addTwoWayEdge(node.id, tl.id, (float)Math.Sqrt(2));
            }
            if (nodeMap.ContainsKey(tr.id))
            {
                this.addTwoWayEdge(node.id, tr.id, 1);
            }
            if (nodeMap.ContainsKey(top.id))
            {
                this.addTwoWayEdge(node.id, top.id, (float)Math.Sqrt(2));
            }
        }

        private void leftEdge(Node node)
        {
            float x = (float)node.position.X;
            float y = (float)node.position.Y;
            Node left = new Node(x - nodeDistance, y);
            if (nodeMap.ContainsKey(left.id))
            {
                this.addTwoWayEdge(node.id, left.id, 1);
            }
        }

        private void rightEdge(Node node)
        {
            float x = (float)node.position.X;
            float y = (float)node.position.Y;
            Node right = new Node(x + nodeDistance, y);
            if (nodeMap.ContainsKey(right.id))
            {
                this.addTwoWayEdge(node.id, right.id, 1);
            }
        }

        private void botEdges(Node node)
        {
            float x = (float)node.position.X;
            float y = (float)node.position.Y;
            Node bl = new Node(x + nodeDistance, y + nodeDistance);
            Node bot = new Node(x, y + nodeDistance);
            Node br = new Node(x + nodeDistance, y + nodeDistance);
            if (nodeMap.ContainsKey(bl.id))
            {
                this.addTwoWayEdge(node.id, bl.id, (float)Math.Sqrt(2));
            }
            if (nodeMap.ContainsKey(bot.id))
            {
                this.addTwoWayEdge(node.id, bot.id, 1);
            }
            if (nodeMap.ContainsKey(br.id))
            {
                this.addTwoWayEdge(node.id, br.id, (float)Math.Sqrt(2));
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
            Node top = new Node(x, y - nodeDistance);
            Node left = new Node(x - nodeDistance, y);
            Node right = new Node(x + nodeDistance, y);
            Node bot = new Node(x, y + nodeDistance);

            if (checkNode(bot))
            {
                nodeMap.Add(bot.id, bot);

                FloodFill(bot);
            }
            if (checkNode(top))
            {
                nodeMap.Add(top.id, top);

                FloodFill(top);
            }
            if (checkNode(left))
            {
                nodeMap.Add(left.id, left);

                FloodFill(left);
            }
            if (checkNode(right))
            {
                nodeMap.Add(right.id, right);

                FloodFill(right);
            }
            Edges(current);
        }


        private bool checkNode(Node node)
        {
            float x = (float)node.position.X;
            float y = (float)node.position.Y;
            List<Obstacle> obstacles = world.getNearbyObstacles(maxRadius + 5, node.position);
            foreach (Obstacle obstacle in obstacles)
            {

                if (Vector2D.Distance(node.position, obstacle.Pos) < obstacle.Radius + 10)
                {
                    return false;
                }
            }
            if (x < 0 || y < 0 || x > world.Width || y > world.Height || nodeMap.ContainsKey(node.id))
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
            foreach (KeyValuePair<string, Node> n in nodeMap)
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
