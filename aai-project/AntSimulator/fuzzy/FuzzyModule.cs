using System;
using System.Collections.Generic;

namespace AntSimulator.fuzzy
{
    public class FuzzyModule
    {
        private Dictionary<string, FuzzyVariable> _variables = new Dictionary<string, FuzzyVariable>();

        private List<FuzzyRule> _rules = new List<FuzzyRule>();

        public FuzzyVariable CreateFlv(string name)
        {
            _variables[name] = new FuzzyVariable();
            return _variables[name];
        }

        public void AddRule(FuzzyTerm antecedent, FuzzyTerm consequence)
        {
            FuzzyRule rule = new FuzzyRule(antecedent, consequence);
            _rules.Add(rule);
        }

        public void Fuzzify(string name, double val)
        {
            if (!_variables.ContainsKey(name))
            {
                throw new ArgumentException("Key not found");
            }
            _variables[name].Fuzzify(val);
        }

        private void SetConfidencesOfConsequentsToZero()
        {
            foreach (var curRule in _rules)
            {
                curRule.SetConfidenceOfConsequentToZero();
            }
        }
        public double Defuzzify(string name)
        {
            if (!_variables.ContainsKey(name))
            {
                throw new ArgumentException("Key not found");
            }
            SetConfidencesOfConsequentsToZero();
            foreach (var rule in _rules)
            {
                rule.Calculate();
            }
            return _variables[name].DefuzzifyMaxAv();

        }

    }
}
