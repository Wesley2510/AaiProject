using AntSimulator.entity;
using AntSimulator.util;
using AntSimulator.world;
using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;

namespace AntSimulator.graph
{
    public class Graph
    {
        private Dictionary<string, Node> _nodeMap = new Dictionary<string, Node>();
        private World _world;
        private const int NodeDistance = 15;
        public Node StartingNode;
        public bool IsBusy;
        
        public Graph(World pWorld)
        {
            _world = pWorld;
            StartingNode = new Node(20, 20);
            float maxRadius = 0;
            foreach (Obstacle obstacle in _world.Obstacles)
            {
                maxRadius = (maxRadius < obstacle.Radius ? obstacle.Radius : maxRadius);
            }

        }


        private void AddEdge(string pName1, string pName2, float pCost)
        {
            Node v = _nodeMap[pName1];
            Node w = _nodeMap[pName2];
            foreach (Edge edge in v.AdjEdges)
            {
                if (edge.Destination == w)
                {
                    return;
                }
            }
            v.AdjEdges.Add(new Edge(w, pCost));
            w.AdjEdges.Add(new Edge(v, pCost));
        }


        private void TopEdge(Node pNode)
        {
            var x = (float)pNode.Position.X;
            var y = (float)pNode.Position.Y;
            Node topleft = new Node(x + NodeDistance, y - NodeDistance);
            Node top = new Node(x, y - NodeDistance);
            Node topright = new Node(x + NodeDistance, y - NodeDistance);
            if (_nodeMap.ContainsKey(topleft.Id))
            {
                AddEdge(pNode.Id, topleft.Id, (float)Math.Sqrt(2));
            }
            if (_nodeMap.ContainsKey(topright.Id))
            {
                AddEdge(pNode.Id, topright.Id, 1);
            }
            if (_nodeMap.ContainsKey(top.Id))
            {
                AddEdge(pNode.Id, top.Id, (float)Math.Sqrt(2));
            }
        }

        private void LeftEdge(Node pNode)
        {
            var x = (float)pNode.Position.X;
            var y = (float)pNode.Position.Y;
            Node left = new Node(x - NodeDistance, y);
            if (_nodeMap.ContainsKey(left.Id))
            {
                AddEdge(pNode.Id, left.Id, 1);
            }
        }

        private void RightEdge(Node pNode)
        {
            var x = (float)pNode.Position.X;
            var y = (float)pNode.Position.Y;
            Node right = new Node(x + NodeDistance, y);
            if (_nodeMap.ContainsKey(right.Id))
            {
                AddEdge(pNode.Id, right.Id, 1);
            }
        }

        private void BotEdges(Node pNode)
        {
            var x = (float)pNode.Position.X;
            var y = (float)pNode.Position.Y;
            Node bl = new Node(x + NodeDistance, y + NodeDistance);
            Node bot = new Node(x, y + NodeDistance);
            Node br = new Node(x + NodeDistance, y + NodeDistance);
            if (_nodeMap.ContainsKey(bl.Id))
            {
                AddEdge(pNode.Id, bl.Id, (float)Math.Sqrt(2));
            }
            if (_nodeMap.ContainsKey(bot.Id))
            {
                AddEdge(pNode.Id, bot.Id, 1);
            }
            if (_nodeMap.ContainsKey(br.Id))
            {
                AddEdge(pNode.Id, br.Id, (float)Math.Sqrt(2));
            }
        }

        private void Edges(Node pNode)
        {
            TopEdge(pNode);
            LeftEdge(pNode);
            RightEdge(pNode);
            BotEdges(pNode);
        }
        public void GenerateGraph(Node pCurrent)
        {
            var x = (float)pCurrent.Position.X;
            var y = (float)pCurrent.Position.Y;
            Node top = new Node(x, y - NodeDistance);
            Node left = new Node(x - NodeDistance, y);
            Node right = new Node(x + NodeDistance, y);
            Node bottom = new Node(x, y + NodeDistance);

            if (CheckNode(bottom))
            {
                _nodeMap.Add(bottom.Id, bottom);

                GenerateGraph(bottom);
            }
            if (CheckNode(top))
            {
                _nodeMap.Add(top.Id, top);

                GenerateGraph(top);
            }
            if (CheckNode(left))
            {
                _nodeMap.Add(left.Id, left);
                GenerateGraph(left);
            }
            if (CheckNode(right))
            {
                _nodeMap.Add(right.Id, right);
                GenerateGraph(right);
            }
            Edges(pCurrent);
        }


        private bool CheckNode(Node pNode)
        {
            var x = pNode.Position.X;
            var y = pNode.Position.Y;
            List<Obstacle> obstacles = _world.Obstacles;
            foreach (Obstacle obstacle in obstacles)
            {
                if (Vector2D.Distance(pNode.Position, new Vector2D(obstacle.Pos.X + (obstacle.Radius), obstacle.Pos.Y + (obstacle.Radius))) < obstacle.Radius  + 10)
                {
                    return false;
                }
            }
            if (x < 0 || y < 0 || x > _world.Width || y > _world.Height || _nodeMap.ContainsKey(pNode.Id))
            {
                return false;
            }

            return true;
        }

        public void ClearAll()
        {
            foreach (KeyValuePair<string, Node> keyValuePair in _nodeMap)
            {
                keyValuePair.Value.Reset();
            }
        }
        public void AStar(Graph graph, string startName, string goalName)
        {
            int i = 0;
            var priorityQueue = new SimplePriorityQueue<Path>();
            Node start = _nodeMap[startName];

            if (_nodeMap[startName] == null)
            {
                throw new ArgumentNullException("No such node found");
            }
            int nodesSeen = 0;
            ClearAll();
            float cost = (float)Vector2D.Distance(start.Position, _nodeMap[goalName].Position);
            var startpath = new Path(start, cost);
            priorityQueue.Enqueue(startpath, startpath.Cost);
            start.Dist = 0;

            while ((priorityQueue.Any()) && (nodesSeen < _nodeMap.Count)) //check  goal
            {
                var vrec = priorityQueue.Dequeue();
                Node v = vrec.Destination;
                if (v == _nodeMap[goalName])
                {
                    break;
                }
                if (v.Scratch != 0)
                {
                    continue;
                }
                v.Scratch = 1;
                nodesSeen++;

                foreach (Edge e in v.AdjEdges)
                {
                    Node w = e.Destination;
                    float cvw = e.Cost;

                    if (cvw < 0)
                    {
                        throw new ArgumentNullException("Graph has negative Edges");
                    }
                    if (w.Dist > v.Dist + cvw)
                    {
                        w.Dist = v.Dist + cvw;
                        w.Prev = v;
                        cost = (float)Vector2D.Distance(w.Position, _nodeMap[goalName].Position);
                        Path newpath = new Path(w, cost);
                        priorityQueue.Enqueue(newpath, newpath.Cost);
                    }
                }
            }
        }
        public List<Vector2D> getRoute(Vector2D currentLocation, Vector2D Destination)
        {
            List<Vector2D> routeList = new List<Vector2D>();
            if (IsBusy) return routeList;
            IsBusy = true;
            string nearestNodeToCurrentLocation = OptimalNode(FindNearbyNodes(currentLocation), Destination).Id;
            string nearestNodeToDestination = OptimalNode(FindNearbyNodes(Destination), currentLocation).Id;
            AStar(this, nearestNodeToCurrentLocation, nearestNodeToDestination);
            var beginNode = _nodeMap[nearestNodeToCurrentLocation];
            var currentNode = _nodeMap[nearestNodeToDestination];
            if (beginNode == null || currentNode == null) return routeList;
            while (beginNode != null && currentNode != beginNode)
            {
                routeList.Add(currentNode.Position);
                currentNode = currentNode.Prev;
            }
            routeList.Add(beginNode.Position);
            IsBusy = false;
            return routeList;
        }

        private Node OptimalNode(Stack<Node> possibleNodes, Vector2D target)
        {
            Node optimalNode = possibleNodes.Pop();
            Node candidate;
            while (possibleNodes.Count != 0)
            {
                candidate = possibleNodes.Pop();
                optimalNode = Vector2D.Distance(optimalNode.Position, target) <
                              Vector2D.Distance(candidate.Position, target)
                    ? optimalNode
                    : candidate;
            }
            return optimalNode;
        }

        public Stack<Node> FindNearbyNodes(Vector2D position)
        {
            Stack<Node> nodes = new Stack<Node>();
            float offsetX = ((float)StartingNode.Position.X % NodeDistance) - NodeDistance;
            float offsetY = ((float)StartingNode.Position.Y % NodeDistance) - NodeDistance;
            int amountOfX = (int)Math.Floor(position.X / NodeDistance);
            int amountOfY = (int)Math.Floor(position.Y / NodeDistance);
            float x = (amountOfX * NodeDistance) + offsetX;
            float y = (amountOfY * NodeDistance) + offsetY;
            string topLeft = "" + x + ',' + y + "";
            string topRight = "" + (x + NodeDistance) + ',' + y + "";
            string bottomLeft = "" + x + ',' + (y + NodeDistance) + "";
            string bottomRight = "" + (x + NodeDistance) + ',' + (y + NodeDistance) + "";
            Node currentNode;
            if (_nodeMap.TryGetValue(topLeft, out currentNode))
            {
                nodes.Push(currentNode);
            }
            if (_nodeMap.TryGetValue(topRight, out currentNode))
            {
                nodes.Push(currentNode);
            }
            if (_nodeMap.TryGetValue(bottomLeft, out currentNode))
            {
                nodes.Push(currentNode);
            }
            if (_nodeMap.TryGetValue(bottomRight, out currentNode))
            {
                nodes.Push(currentNode);
            }
            return nodes;
        }

        public void Render(Graphics G)
        {
            var nodepen = new Pen(Color.Blue, 2f);
            var edgeTested = new Pen(Color.Green, 2f);
            var edgePen = new Pen(Color.LightGray, 1f);
            foreach (KeyValuePair<string, Node> n in _nodeMap)
            {
                foreach (Edge e in n.Value.AdjEdges)
                {
                    if (n.Value.Scratch == 0)
                    {
                        G.DrawLine(edgePen, (float)n.Value.Position.X, (float)n.Value.Position.Y, (float)e.Destination.Position.X, (float)e.Destination.Position.Y);
                    }
                    else
                    {
                        G.DrawLine(edgeTested, (float)n.Value.Position.X, (float)n.Value.Position.Y, (float)e.Destination.Position.X,
                            (float)e.Destination.Position.Y);
                    }
                }
                G.DrawEllipse(nodepen, (float)n.Value.Position.X - 2, (float)n.Value.Position.Y - 2, 4, 4);
            }
        }
    }
}
