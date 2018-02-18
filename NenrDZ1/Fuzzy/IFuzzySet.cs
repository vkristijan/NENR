using System;
using System.Text;
using NenrDZ1.Domains;

namespace NenrDZ1.Fuzzy
{
    public interface IFuzzySet
    {
        IDomain GetDomain();
        double GetValueAt(DomainElement element);
    }

    public abstract class FuzzySet : IFuzzySet
    {
        protected IDomain Domain;
        public IDomain GetDomain() => Domain;

        public abstract double GetValueAt(DomainElement element);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var element in Domain)
            {
                sb.Append("d(")
                    .Append(element)
                    .Append(")=")
                    .Append(GetValueAt(element).ToString("0.0000"))
                    .Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
