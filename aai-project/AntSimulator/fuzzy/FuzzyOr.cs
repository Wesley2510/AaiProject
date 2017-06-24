using System.Collections.Generic;

namespace AntSimulator.fuzzy
{
    public class FzOr : FuzzyTerm
    {
        private List<FuzzyTerm> _terms = new List<FuzzyTerm>();

        public FzOr(FzOr fa)
        {
            foreach (var current in fa._terms)
            {
                _terms.Add(current);
            }
        }

        public FzOr(FuzzyTerm op1, FuzzyTerm op2)
        {
            _terms.Add(op1);
            _terms.Add(op2);
        }

        public FzOr(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3)
        {
            _terms.Add(op1.Clone());
            _terms.Add(op2.Clone());
            _terms.Add(op3.Clone());
        }

        public FzOr(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3, FuzzyTerm op4)
        {
            _terms.Add(op1.Clone());
            _terms.Add(op2.Clone());
            _terms.Add(op3.Clone());
            _terms.Add(op4.Clone());
        }

        public override void ClearDom()
        {
            foreach (var curTerm in _terms)
            {
                curTerm.ClearDom();
            }
        }

        public override FuzzyTerm Clone()
        {
            return new FzOr(this);
        }

        public override double GetDom()
        {

            double largest = double.MinValue;
            foreach (var curTerm in _terms)
            {
                if (curTerm.GetDom() > largest)
                {
                    largest = curTerm.GetDom();
                }
            }


            return largest;
        }

        public override void ORwithDom(double value) { }
    }
}
