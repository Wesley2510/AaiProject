using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Graph
{
    public class Node
    {
        protected int _index { get; set; }       

        public Node(int index)
        {
            if (index < 0)
                _index = 0;
            else
                _index = index;
        }

        public int Index()
        {
            return _index;
        }
        
    }
}
