namespace AntSimulator.fuzzy
{
    public class FuzzyRule
    {
        private FuzzyTerm _antecedent;
        private FuzzyTerm _consequent;

        public FuzzyRule(FuzzyTerm ant, FuzzyTerm con)
        {
            _antecedent = ant.Clone();
            _consequent = con.Clone();
        }

        public void SetConfidenceOfConsequentToZero()
        {
            _consequent.ClearDom();
        }

        public void Calculate()
        {
            _consequent.ORwithDom(_antecedent.GetDom());
        }
    }
}
