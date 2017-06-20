using System.Collections.Generic;
using AntSimulator.util;

namespace AntSimulator.GraphPath
{

    public class Node
    {
        public string id;
        public Vector2D position;
        public List<Edge> adjEdges;
        public float dist;
        public Node prev;
        public int scratch;

        public Node(float x, float y)
        {
            id = "" + x + ',' + y + "";
            position = new Vector2D(x, y);
            adjEdges = new List<Edge>();
        }
    }
}
