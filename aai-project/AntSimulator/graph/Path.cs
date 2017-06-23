namespace AntSimulator.graph
{
    public class Path
    {
        public Node Destination;
        public float Cost;

        public Path(Node destination, float cost)
        {
            Destination = destination;
            Cost = cost;
        }
    }
}
