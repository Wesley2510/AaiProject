namespace AntSimulator.fuzzy
{
    public abstract class FuzzyTerm
    {
        public abstract FuzzyTerm Clone();

        public abstract double GetDom();

        public abstract void ClearDom();

        public abstract void ORwithDom(double value);
    }
}
