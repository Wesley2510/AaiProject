using System;

namespace SteeringCS.graphs
{
    public class Path : IComparable<Path>
    {
        public float Cost;
        public Node Dest;

        public Path(Node dest, float cost)
        {
            Cost = cost;
            Dest = dest;
        }

        public int CompareTo(Path other)
        {
            double otherCost = other.Cost;
            return Cost < otherCost ? -1 : Cost > otherCost ? 1 : 0;
        }
    }
}
