using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.fuzzy
{
    abstract class FuzzySet
    {
        public abstract double CalculateDOM(double value);
        public abstract void GetDOM();
        public abstract void ClearDOM();
        public abstract void ORwithDOM(double value);

    }
}
