namespace SteeringCS.graphs
{
    public class Edge
    {
        public float Cost;
        public Node Dest;

        public Edge(float cost, Node dest)
        {
            Dest = dest;
            Cost = cost;
        }
    }
}