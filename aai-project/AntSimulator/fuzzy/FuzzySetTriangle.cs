namespace AntSimulator.fuzzy
{
    public class FuzzySetTriangle : FuzzySet
    {
        private readonly double _peakPoint;
        private readonly double _leftOffset;
        private readonly double _rightOffset;

        public FuzzySetTriangle(double mid, double lft, double rgt) : base(repVal: mid)
        {

            _leftOffset = lft;
            _peakPoint = mid;
            _rightOffset = rgt;
        }


        public override double CalculateDom(double val)
        {
            //test for the case where the triangle's left or right offsets are zero
            if (_rightOffset == 0.0 && _peakPoint == val || _leftOffset == 0.0 && _peakPoint == val)
            {
                return 1.0;
            }

            //find DOM if left of center
            if (val <= _peakPoint && val > _peakPoint - _leftOffset)
            {
                double grad = 1.0 / _leftOffset;

                return grad * (val - (_peakPoint - _leftOffset));
            }
            //find DOM if right of center
            if (val > _peakPoint && val < _peakPoint + _rightOffset)
            {
                double grad = 1.0 / -_rightOffset;

                return grad * (val - _peakPoint) + 1.0;
            }
            //out of range of this FLV, return zero
            return 0.0;
        }

        public double RepresentativeValue()
        {
            return _peakPoint;
        }
    }
}
