using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.util;

namespace SteeringCS.Graph
{
    public class GraphNode : Node
    {
        public Vector2D Target;

        public GraphNode(Vector2D target, int index) : base(index)
        {
            Target = target;
        }


    }
}
