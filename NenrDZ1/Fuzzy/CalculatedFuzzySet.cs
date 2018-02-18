using NenrDZ1.Domains;

namespace NenrDZ1.Fuzzy
{
    public class CalculatedFuzzySet : FuzzySet
    {
        private readonly IntUnaryFunction _function;

        public CalculatedFuzzySet(IDomain domain, IntUnaryFunction function)
        {
            Domain = domain;
            _function = function;
        }

        public override double GetValueAt(DomainElement element) => 
            _function(element[0]);
    }
}
