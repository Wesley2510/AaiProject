namespace AntSimulator.fuzzy
{
    public class FzVery : FuzzyTerm
    {
        private FuzzySet _set;

        private FzVery(FzVery inst)
        {
            _set = inst._set;
        }
        public FzVery(FzSet inst)
        {
            _set = inst.GetSet();
        }
        public override FuzzyTerm Clone()
        {
            return new FzVery(this);
        }

        public override double GetDom()
        {
            return _set.GetDom() * _set.GetDom();
        }

        public override void ClearDom()
        {
            _set.ClearDom();
        }

        public override void ORwithDom(double val)
        {
            _set.ORwithDom(val * val);
        }
    }
}
