namespace AntSimulator.fuzzy.concrete
{
    class FuzzyConcreteThirst : FuzzySystem
    {
        public FuzzyModule FuzzyModule;

        public FuzzyConcreteThirst()
        {
            FuzzyModule = new FuzzyModule();
            Init();
        }
        public override void Init()
        {
            FuzzyVariable thirst = FuzzyModule.CreateFlv("Thirst");
            FzSet thirstLow = thirst.AddLeftShoulderSet("Thirst Low", 0, 20, 50);
            FzSet thirstMedium = thirst.AddTriangularSet("Thirst Medium", 20, 50, 80);
            FzSet thirstHigh = thirst.AddRightShoulderSet("Thirst High", 50, 80, 100);

            FuzzyVariable Desirability = FuzzyModule.CreateFlv("Desirability");

            FzSet undesirable = Desirability.AddLeftShoulderSet("Undesirable", 0, 0.25f, 0.5f);
            FzSet desirable = Desirability.AddTriangularSet("Desirable", 0.25f, 0.5f, 0.75f);
            FzSet veryDesirable = Desirability.AddRightShoulderSet("VeryDesirable", 0.5f, 0.75f, 1);

            FuzzyVariable hapiness = FuzzyModule.CreateFlv("Hapiness");

            FzSet hapiness_low = hapiness.AddLeftShoulderSet("Not happy", 0, 10, 25);
            FzSet hapiness_medium = hapiness.AddLeftShoulderSet("Happy", 10, 25, 50);
            FzSet hapiness_high = hapiness.AddLeftShoulderSet("Very happy", 25, 50, 100);

            FuzzyModule.AddRule(new FzAnd(thirstLow, hapiness_low), undesirable);
            FuzzyModule.AddRule(new FzAnd(thirstLow, hapiness_medium), undesirable);
            FuzzyModule.AddRule(new FzAnd(thirstLow, hapiness_high), veryDesirable);

            FuzzyModule.AddRule(new FzAnd(thirstMedium, hapiness_low), undesirable);
            FuzzyModule.AddRule(new FzAnd(thirstMedium, hapiness_medium), desirable);
            FuzzyModule.AddRule(new FzAnd(thirstMedium, hapiness_high), desirable);

            FuzzyModule.AddRule(new FzAnd(thirstHigh, hapiness_low), undesirable);
            FuzzyModule.AddRule(new FzAnd(thirstHigh, hapiness_medium), desirable);
            FuzzyModule.AddRule(new FzAnd(thirstHigh, hapiness_high), undesirable);
        }
    }
}
