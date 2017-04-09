using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SteeringCS.graphs.priorityqueue;
using SteeringCS.util;

namespace SteeringCS.graphs
{
    public class Graph
    {
        public const float Infinity = (float) double.MaxValue;
        public Dictionary<int, Node> NodeMap = new Dictionary<int, Node>();

        public Node GetNode(int nodeIndex, Vector2D vector2D)
        {
            Node node;

            if (!NodeMap.ContainsKey(nodeIndex))
            {
                node = new Node(nodeIndex, vector2D);
                NodeMap.Add(nodeIndex, node);
            }
            else
            {
                node = NodeMap[nodeIndex];
            }
            return node;
        }

        public void AddEdge(int source, Vector2D sourceVec, int destination, Vector2D destVec, float cost)
        {
            var v = GetNode(source, sourceVec);
            var w = GetNode(destination, destVec);
            v.Adj.AddLast(new Edge(cost, w));
        }

        private void ClearAll()
        {
            foreach (var keyValuePair in NodeMap)
            {
                keyValuePair.Value.Reset();
            }
        }

        private static void PrintPath(Node dest)
        {
            if (dest.Prev != null)
            {
                PrintPath(dest.Prev);
                Console.WriteLine(" to");
            }
            Console.WriteLine(dest.Index);
        }

        public void PrintPath(int dest)
        {
            if (NodeMap[dest] == null)
            {
                throw new ArgumentException();
            }
            var w = NodeMap[dest];
            if (float.IsPositiveInfinity(w.Dist))
            {
                Console.WriteLine(dest + " is unreachable");
            }
            else
            {
            Console.WriteLine("(Cost is: " + w.Dist + " )");
                PrintPath(w);
                Console.WriteLine();
            }
        }

        private static float HeuristicEuclid(Vector2D v1, Vector2D v2)
        {
            return (float)Vector2D.Distance(v1, v2);
        }

        public void Astar(int startIndex, int destination)
        {
            var pq = new SimplePriorityQueue<Path>();
            var startNode = NodeMap[startIndex];

            if (NodeMap[startIndex] == null)
            {
                throw new ArgumentException("No such vertex found");
            }
            int nodesSeen = 0;
            ClearAll();
            float cost = HeuristicEuclid(startNode.Postition, NodeMap[destination].Postition);
            var startPath = new Path(startNode, cost);
            pq.Enqueue(startPath, startPath.Cost);
            startNode.Dist = 0;

            while ((pq.Any()) && (nodesSeen < NodeMap.Count))
            {
                Path vrec = pq.Dequeue();
                Node v = vrec.Dest;
                if (v == NodeMap[destination])
                {
                    break;
                }
                if (v.Scratch != 0)
                {
                    continue;
                }
                v.Scratch = 1;
                nodesSeen++;
                foreach (var e in v.Adj)
                {
                    Node w = e.Dest;
                    float cvw = e.Cost;
                    if (cvw < 0)
                    {
                        throw new ArgumentNullException("Graph has negative edges");
                    }
                    if (w.Dist > v.Dist + cvw)
                    {
                        w.Dist = v.Dist + cvw;
                        w.Prev = v;
                        cost = HeuristicEuclid(w.Postition, NodeMap[destination].Postition);
                        var newPath = new Path(w,cost);
                        pq.Enqueue(newPath, newPath.Cost);
                    }
                }
            }
        }

        public void Dijkstra(int startIndex)
        {
            var pq = new SimplePriorityQueue<Path>();

            if (NodeMap[startIndex] == null)
            {
                throw new ArgumentException();
            }

            var start = NodeMap[startIndex];
            var cost = 0;
            ClearAll();
            start.Dist = 0;
            var newPath = new Path( start,cost);
            pq.Enqueue(newPath, newPath.Cost);
            int nodesSeen = 0;

            while ((pq.Any()) && (nodesSeen < NodeMap.Count))
            {
                var vrec = pq.Dequeue();
                var v = vrec.Dest;
                if (v.Scratch != 0)
                {
                    continue;
                }
                v.Scratch = 1;
                nodesSeen++;

                foreach (var e in v.Adj)
                {
                    var w = e.Dest;
                    var cvw = e.Cost;

                    if (cvw < 0)
                    {
                        throw new GraphException("Graph has negative edges");
                    }

                    if (w.Dist > v.Dist + cvw)
                    {
                        w.Dist = v.Dist + cvw;
                        w.Prev = v;
                        Path p = new Path(w, w.Dist);
                        pq.Enqueue(p, p.Cost);
                    }
                }
            }
        }


        public void Unweighted(int startName)
        {
            ClearAll();
            if (NodeMap[startName] == null)
            {
                throw new GraphException("Start vertex not found");
            }

            var start = NodeMap[startName];
            Queue<Node> q = new Queue<Node>();
            start.Dist = 0;
            q.Enqueue(start);

            while (q.Count() != 0)
            {
                var v = q.Dequeue();

                foreach (var e in v.Adj)
                {
                    var w = e.Dest;
                    if (float.IsPositiveInfinity(w.Dist))
                    {
                        w.Dist = v.Dist + 1;
                        w.Prev = v;
                        q.Enqueue(w);
                    }
                }
            }
        }

        public bool IsConnected()
        {
            bool isConnect = true;
            Unweighted(NodeMap.Keys.First());
            foreach (var node in NodeMap.Values)
            {
                if (float.IsPositiveInfinity(node.Dist))
                {
                    isConnect = false;
                }
            }
            return isConnect;
        }

        public void Render(Graphics g, Node node)
        {
            g.FillEllipse(Brushes.Chartreuse, new Rectangle((int)node.Postition.X, (int)node.Postition.Y, 5, 5));
        }
    }

    internal class GraphException : SystemException
    {
        public GraphException(string name) : base(name)
        {
        }
    }
}