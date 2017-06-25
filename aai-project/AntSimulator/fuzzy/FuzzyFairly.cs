using System;

namespace AntSimulator.fuzzy
{
    public class FzFairly : FuzzyTerm
    {
        private FuzzySet _set;

        private FzFairly(FzFairly inst)
        {
            _set = inst._set;
        }

        public FzFairly(FzSet inst)
        {
            _set = inst.GetSet();
        }

        public override FuzzyTerm Clone()
        {
            return new FzFairly(this);
        }

        public override double GetDom()
        {
            return Math.Sqrt(_set.GetDom());
        }

        public override void ClearDom()
        {
            _set.ClearDom();
        }

        public override void ORwithDom(double val)
        {
            _set.ORwithDom(Math.Sqrt(val));
        }
    }
}
