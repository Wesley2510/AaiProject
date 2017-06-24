using System.Collections.Generic;

namespace AntSimulator.fuzzy
{
    public class FzAnd : FuzzyTerm
    {
        private List<FuzzyTerm> _terms = new List<FuzzyTerm>();
        public FzAnd(FzAnd fzAnd)
        {
            foreach (var curTerm in fzAnd._terms)
            {
                _terms.Add(curTerm.Clone());
            }
        }

        public FzAnd(FuzzyTerm op1, FuzzyTerm op2)
        {
            _terms.Add(op1.Clone());
            _terms.Add(op2.Clone());
        }

        public FzAnd(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3)
        {
            _terms.Add(op1.Clone());
            _terms.Add(op2.Clone());
            _terms.Add(op3.Clone());
        }

        public FzAnd(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3, FuzzyTerm op4)
        {
            _terms.Add(op1.Clone());
            _terms.Add(op2.Clone());
            _terms.Add(op3.Clone());
            _terms.Add(op4.Clone());
        }

        public override FuzzyTerm Clone()
        {
            return new FzAnd(this);
        }

        public override double GetDom()
        {
            double smallest = double.MaxValue;
            foreach (var curTerm in _terms)
            {
                if (curTerm.GetDom() < smallest)
                {
                    smallest = curTerm.GetDom();

                }
            }
            return smallest;
        }

        public override void ClearDom()
        {
            foreach (var curTerm in _terms)
            {
                curTerm.ClearDom();
            }
        }

        public override void ORwithDom(double val)
        {
            foreach (var curTerm in _terms)
            {
                curTerm.ORwithDom(val);
            }
        }
    }
}
