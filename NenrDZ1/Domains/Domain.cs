using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NenrDZ1.Domains
{
    public abstract class Domain : IDomain
    {
        public abstract int GetCardinality();
        public abstract IDomain GetComponent(int index);
        public IDomain this[int i] => GetComponent(i);
        public abstract int GetNumberOfComponents();

        public static IDomain IntRange(int first, int last)
        {
            return new SimpleDomain(first, last);
        }

        public static IDomain Combine(IDomain first, IDomain second)
        {
            var domains = new List<SimpleDomain>();

            for (var i = 0; i < first.GetNumberOfComponents(); ++i)
            {
                domains.Add(first[i] as SimpleDomain);
            }
            for (var i = 0; i < second.GetNumberOfComponents(); ++i)
            {
                domains.Add(second[i] as SimpleDomain);
            }

            return new CompositeDomain(domains.ToArray());
        }

        public abstract int IndexOfElement(DomainElement element);
        public bool HasElement(DomainElement element)
        {
            try
            {
                IndexOfElement(element);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public abstract DomainElement ElementForIndex(int index);

        public abstract IEnumerator<DomainElement> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var element in this)
            {
                sb.Append("Element domene: ");
                sb.Append(element);
                sb.Append(Environment.NewLine);
            }
            sb.Append("Kardinalitet domene je: ");
            sb.Append(GetCardinality());
            return sb.ToString();
        }
    }
}
