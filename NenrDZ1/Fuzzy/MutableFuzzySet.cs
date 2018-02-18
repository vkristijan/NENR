using NenrDZ1.Domains;

namespace NenrDZ1.Fuzzy
{
    public class MutableFuzzySet : FuzzySet
    {
        private readonly double[] _memberships;

        public MutableFuzzySet(IDomain domain)
        {
            Domain = domain;
            _memberships = new double[Domain.GetCardinality()];
        }

        public override double GetValueAt(DomainElement element) => 
            _memberships[Domain.IndexOfElement(element)];

        public MutableFuzzySet Set(DomainElement element, double value)
        {
            _memberships[Domain.IndexOfElement(element)] = value;
            return this;
        }
    }
}
