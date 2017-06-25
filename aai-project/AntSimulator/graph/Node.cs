using AntSimulator.util;
using System.Collections.Generic;

namespace AntSimulator.graph
{

    public class Node
    {
        public string Id;
        public Vector2D Position;
        public List<Edge> AdjEdges;
        public float Dist;
        public Node Prev;
        public int Scratch;

        public Node(float x, float y)
        {
            Id = "" + x + ',' + y + "";
            Position = new Vector2D(x, y);
            AdjEdges = new List<Edge>();
        }
        public void Reset()
        {
            Dist = int.MaxValue;
            Prev = null;
            Scratch = 0;
        }
    }
}
