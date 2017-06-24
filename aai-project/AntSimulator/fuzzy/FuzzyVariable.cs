using System;
using System.Collections.Generic;

namespace AntSimulator.fuzzy
{
    public class FuzzyVariable
    {
        private Dictionary<string, FuzzySet> _memberSets = new Dictionary<string, FuzzySet>();
        private double _minRange;
        private double _maxRange;

        private void AdjustRangeToFit(double min, double max)
        {
            if (min < _minRange)
                _minRange = min;
            if (max > _maxRange)
                _maxRange = max;
        }

        public FzSet AddTriangularSet(string name,
            double minBound,
            double peak,
            double maxBound)
        {
            _memberSets[name] = new FuzzySetTriangle(peak,
                peak - minBound,
                maxBound - peak);
            //adjust range if necessary
            AdjustRangeToFit(minBound, maxBound);

            return new FzSet(_memberSets[name]);
        }

        public FzSet AddRightShoulderSet(string name,
            double minBound,
            double peak,
            double maxBound)
        {
            _memberSets[name] = new FuzzySetRightShoulder(peak, peak - minBound, maxBound - peak);

            //adjust range if necessary
            AdjustRangeToFit(minBound, maxBound);

            return new FzSet(_memberSets[name]);
        }
        public FzSet AddLeftShoulderSet(string name,
            double minBound,
            double peak,
            double maxBound)
        {
            _memberSets[name] = new FuzzySetLeftShoulder(peak, peak - minBound, maxBound - peak);

            //adjust range if necessary
            AdjustRangeToFit(minBound, maxBound);

            return new FzSet(_memberSets[name]);
        }
        public FzSet AddSingletonSet(string name,
            double minBound,
            double peak,
            double maxBound)
        {
            _memberSets[name] = new FuzzySetSingleton(peak,
                peak - minBound,
                maxBound - peak);

            AdjustRangeToFit(minBound, maxBound);

            return new FzSet(_memberSets[name]);
        }

        public void Fuzzify(double val)
        {
            if (val >= _minRange && val <= _maxRange)
            {
                foreach (var curSet in _memberSets)
                {
                    curSet.Value.SetDom(curSet.Value.CalculateDom(val));
                }
            }
            else
            {
                throw new ArgumentException("Invalid value");
            }
        }
        public double DefuzzifyMaxAv()
        {
            double bottom = 0.0;
            double top = 0.0;

            foreach (var curSet in _memberSets)
            {
                bottom += curSet.Value.GetDom();
                top += curSet.Value.GetRepresentativeVal() * curSet.Value.GetDom();
            }

            //make sure bottom is not equal to zero
            if (Math.Abs(bottom) < 0.0)
                return 0.0f;

            return top / bottom;
        }
    }
}
