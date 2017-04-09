using System.Collections.Generic;
using SteeringCS.util;

namespace SteeringCS.graphs
{
    public enum ExtraInfo { HasFood, }
    public class Node
    {
        public int Index;
        public LinkedList<Edge> Adj;
        public Vector2D Postition;
        public float Dist;
        public Node Prev;
        public int Scratch;
        public ExtraInfo ExtraInfo;
        public static int ID = 0;

        public Node(Vector2D position)
        {
            Index = ID;
            ID++;
            Postition = position;
            Adj = new LinkedList<Edge>();
            Reset();
        }

        public Node(int index, Vector2D position)
        {
            Index = index;
            Postition = position;
            Adj = new LinkedList<Edge>();
            Reset();
        }


        public void SetExtraInfo(ExtraInfo extraInfo)
        {
            ExtraInfo = extraInfo;
        }

        public void Reset()
        {
            Dist = Graph.Infinity;
            Prev = null;
            Scratch = 0;
        }
    }
}
