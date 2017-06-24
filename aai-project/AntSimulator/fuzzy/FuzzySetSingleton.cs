namespace AntSimulator.fuzzy
{
    public class FuzzySetSingleton : FuzzySet
    {
        private readonly double _midPoint;
        private readonly double _leftOffset;
        private readonly double _rightOffset;

        public FuzzySetSingleton(double mid, double lft, double rgt) : base(mid)
        {
            _midPoint = mid;
            _leftOffset = lft;
            _rightOffset = rgt;
        }
        public override double CalculateDom(double val)
        {
            if (val >= _midPoint - _leftOffset && val <= _midPoint + _rightOffset)
            {
                return 1.0;
            }
            //out of range of this FLV, return zero
            return 0.0;
        }
    }
}
