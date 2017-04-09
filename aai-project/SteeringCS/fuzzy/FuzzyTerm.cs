using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.fuzzy
{
    public abstract class FuzzyTerm
    {

        public abstract void Clone();
        public abstract void GetDOM();
        public abstract void ClearDOM();
        public abstract void ORwithDOM(double value);
    }
}
