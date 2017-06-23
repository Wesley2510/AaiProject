namespace AntSimulator.fuzzy
{
    public abstract class FuzzySystem
    {
        private static readonly FuzzySystem Instance;
        public FuzzyModule FuzzyModule;
        public abstract void Init();

    }
}
