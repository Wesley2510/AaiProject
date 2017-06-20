namespace AntSimulator.GraphPath
{
    public class Edge
    {
        public Node destination;   
        public float cost;        

        public Edge(Node pDestination, float pCost)
        {
            destination = pDestination;
            cost = pCost;
        }
    }
}
