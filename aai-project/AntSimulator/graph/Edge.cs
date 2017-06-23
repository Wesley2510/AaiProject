namespace AntSimulator.graph
{
    public class Edge
    {
        public Node Destination;
        public float Cost;

        public Edge(Node pDestination, float pCost)
        {
            Destination = pDestination;
            Cost = pCost;
        }
    }
}
