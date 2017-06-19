namespace AntSimulator.GraphPath
{
    public class Edge
    {
        public Node dest;   /* Second Node in Edge */
        public float cost;   // Edge cost        

        public Edge(Node d, float c)
        {
            this.dest = d;
            this.cost = c;
        }
    }
}
