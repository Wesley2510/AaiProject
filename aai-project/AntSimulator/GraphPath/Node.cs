using System.Collections.Generic;
using AntSimulator.util;

namespace AntSimulator.GraphPath
{

    public class Node
    {
        public string id;   // Node name    
        public Vector2D position;
        public List<Edge> adjEdges;    // Adjacent vertices    
        public float dist;
        public Node prev;   // Previous vertex on shortest path
        public int scratch; //dijkstrta

        public Node(float x, float y)
        {
            id = "" + x + ',' + y + "";
            position = new Vector2D(x, y);
            adjEdges = new List<Edge>();
            Reset();
        }

        public void Reset()
        {
            dist = int.MaxValue;
            prev = null;
            scratch = 0;
        }

    }

}
