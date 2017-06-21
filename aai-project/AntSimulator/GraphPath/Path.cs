using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSimulator.GraphPath
{
    public class Path
    {
        public Node dest;
        public float cost;

        public Path(Node d, float c)
        {
            dest = d;
            cost = c;
        }
    }
}