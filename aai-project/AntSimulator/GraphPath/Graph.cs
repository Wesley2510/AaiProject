using System.Collections.Generic;
using System.Drawing;
using AntSimulator.util;
using AntSimulator.world;

namespace AntSimulator.GraphPath
{
    class Graph
    {
        private Dictionary<string, Node> nodeMap = new Dictionary<string, Node>();
        private World world;
        private int height;
        private int width;
        private int nodeDistance = 15;
        private int maxRadius;
        public bool IsBusy;

        public Node startnode;

        public Graph(World pWorld)
        {
            world = pWorld;
            height = world.Height;
            width = world.Width;
            startnode = new Node(20, 20);
            maxRadius = 0;
            //foreach (Obstacle obstacle in linkedWorld.obstacles)
            //{
            //    maxRadius = (maxRadius < obstacle.Radius ? obstacle.Radius : maxRadius);
            //}
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

        //public void FloodFill(Node current)
        //{
        //    float x = (float) current.position.X;
        //    float y = (float) current.position.Y;
        //    Node top = new Node(x, y - nodeDistance);
        //    Node left = new Node(x - nodeDistance, y);
        //    Node right = new Node(x + nodeDistance, y);
        //    Node bot = new Node(x, y + nodeDistance);

        //    if (checkNode(bot))
        //    {
        //        nodeMap.Add(bot.id, bot);

        //        FloodFill(bot);
        //    }
        //    if (checkNode(top))
        //    {
        //        nodeMap.Add(top.id, top);

        //        FloodFill(top);
        //    }
        //    if (checkNode(left))
        //    {
        //        nodeMap.Add(left.id, left);

        //        FloodFill(left);
        //    }
        //    if (checkNode(right))
        //    {
        //        nodeMap.Add(right.id, right);

        //        FloodFill(right);
        //    }
        //    Edges(current);
        //}


        //private bool checkNode(Node node)
        //{
        //    float x = (float) node.position.X;
        //    float y = (float)node.position.Y;
        //    List<Obstacle> obstacles = world.getNearbyObstacles(maxRadius + 5, node.position);
        //    foreach (Obstacle obstacle in obstacles)
        //    {

        //        if (Vector2D
        //            //GetDistanceBetweenVectors(node.position, obstacle.Pos) < obstacle.Radius + 10)
        //        {
        //            return false;
        //        }
        //    }
        //    if (x < 0 || y < 0 || x > width || y > height || nodeMap.ContainsKey(node.id))
        //    {
        //        return false;
        //    }

        //    return true;
        //}
        //public void Render(Graphics G)
        //{
        //    Pen nodepen = new Pen(Color.Blue, 2f);
        //    Pen EdgeTested = new Pen(Color.Purple, 2f);
        //    Pen EdgePen = new Pen(Color.LightGray, 1f);
        //    foreach (KeyValuePair<string, Node> n in nodeMap)
        //    {
        //        foreach (Edge e in n.Value.adjEdges)
        //        {
        //            if (n.Value.scratch == 0)
        //            {
        //                G.DrawLine(EdgePen, n.Value.position.X, n.Value.position.Y, e.dest.position.X, e.dest.position.Y);
        //            }
        //            else
        //            {
        //                G.DrawLine(EdgeTested, n.Value.position.X, n.Value.position.Y, e.dest.position.X,
        //                    e.dest.position.Y);
        //            }
        //        }
        //        G.DrawEllipse(nodepen, n.Value.position.X - 2, n.Value.position.Y - 2, 4, 4);
        //    }
        //}
    }
}
