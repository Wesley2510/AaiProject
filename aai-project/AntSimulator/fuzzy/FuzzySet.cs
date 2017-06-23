using System;

namespace AntSimulator.fuzzy
{
    public abstract class FuzzySet
    {
        protected double Dom;
        protected double RepresentativeValue;

        protected FuzzySet(double repVal)
        {
            Dom = 0.0f;
            RepresentativeValue = repVal;
        }

        public abstract double CalculateDom(double val);

        public double GetRepresentativeVal()
        {
            return RepresentativeValue;
        }

        public double GetDom()
        {
            return Dom;
        }

        public void SetDom(double val)
        {
            if (val <= 1 && val >= 0)
            {
                Dom = val;
            }
            else
            {
                throw new ArgumentException("Invalid value");
            }

        }

        public void ClearDom()
        {
            Dom = 0.0f;
        }

        public void ORwithDom(double val)
        {
            if (val > Dom)
                Dom = val;
        }
    }
}
