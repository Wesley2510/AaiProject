namespace AntSimulator.fuzzy
{
    public class FuzzySetLeftShoulder : FuzzySet
    {
        private readonly double _peakPoint;
        private readonly double _leftOffset;
        private readonly double _rightOffset;
        public FuzzySetLeftShoulder(double peak, double lft, double rgt) : base(repVal: (peak - lft + peak) / 2)
        {
            _leftOffset = lft;
            _peakPoint = peak;
            _rightOffset = rgt;
        }
        public override double CalculateDom(double val)
        {
            //test for the case where the left or right offsets are zero
            //(to prevent divide by zero errors below)
            if (_rightOffset == 0.0 && _peakPoint == val || _leftOffset == 0.0 && _peakPoint == val)
            {
                return 1.0;
            }
            //find DOM if right of center
            if (val >= _peakPoint && val < _peakPoint + _rightOffset)
            {
                double grad = 1.0 / -_rightOffset;

                return grad * (val - _peakPoint) + 1.0;
            }
            //find DOM if left of center
            if (val < _peakPoint && val >= _peakPoint - _leftOffset)
            {
                return 1.0;
            }
            //out of range of this FLV, return zero
            return 0.0;
        }
    }
}
