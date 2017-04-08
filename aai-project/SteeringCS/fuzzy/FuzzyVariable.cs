using System;
using System.Collections.Generic;

namespace SteeringCS.fuzzy
{
    public class FuzzyVariable
    {
        private Dictionary<string, FuzzySet> Variables;

        public FzSet AddTriangleSet(string s, double d, double d2, double d3)
        {
            return new FzSet();
        }

        public FzSet AddRightShoulderSet(string s, double d, double d2, double d3)
        {
            return new FzSet();
        }
        public FzSet AddLeftShoulderSet(string s, double d, double d2, double d3)
        {
            return new FzSet();
        }
        public FzSet AddSingletonSet(string s, double d, double d2, double d3)
        {
            return new FzSet();
        }

        public void Fuzzify(double d)
        {
            
        }

        public float DefuzzifyMaxAV()
        {
            return 1.0f;
        }

        public float DefuzzifyCentroid()
        {
            return 1.0f;
        }

    }
}