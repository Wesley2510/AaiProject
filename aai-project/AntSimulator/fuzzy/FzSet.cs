namespace AntSimulator.fuzzy
{
    public class FzSet : FuzzyTerm
    {
        private FuzzySet _set;


        public FzSet(FuzzySet fs)
        {
            _set = fs;
        }

        public override FuzzyTerm Clone()
        {
            return new FzSet(_set);
        }

        public override double GetDom()
        {
            return _set.GetDom();
        }

        public override void ClearDom()
        {
            _set.ClearDom();
        }

        public override void ORwithDom(double val)
        {
            _set.ORwithDom(val);
        }

        public FuzzySet GetSet()
        {
            return _set;
        }
    }
}
