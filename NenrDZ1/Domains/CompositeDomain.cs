using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NenrDZ1.Domains
{
    public class CompositeDomain : Domain
    {
        private readonly SimpleDomain[] _domains;

        public CompositeDomain(SimpleDomain[] domains)
        {
            _domains = domains;
        }

        public override int GetCardinality()
        {
            return _domains.Aggregate(1, (current, element) => current * element.GetCardinality());
        }

        public override IDomain GetComponent(int index)
        {
            if (index >= GetNumberOfComponents())
            {
                throw new IndexOutOfRangeException();
            }

            return _domains[index];
        }

        public override int GetNumberOfComponents()
        {
            return _domains.Length;
        }

        public override int IndexOfElement(DomainElement element)
        {
            if (element.GetNumberOfComponents() != GetNumberOfComponents())
            {
                throw new Exception("Element not in domain!");
            }

            int index = 0;
            int combinations = 1;
            for (int i = GetNumberOfComponents() - 1; i >= 0; --i)
            {
                DomainElement value = DomainElement.Of(element.GetComponentValue(i));
                index += (combinations * _domains[i].IndexOfElement(value));
                combinations *= _domains[i].GetCardinality();
            }

            return index;
        }

        public override DomainElement ElementForIndex(int index)
        {
            int[] values = new int[GetNumberOfComponents()];

            for (int i = GetNumberOfComponents() - 1; i >= 0; --i)
            {
                int n = _domains[i].GetCardinality();
                values[i] = _domains[i].ElementForIndex(index % n)[0];
                index /= n;
            }

            return DomainElement.Of(values);
        }

        public override IEnumerator<DomainElement> GetEnumerator()
        {
            return new CompositeEnumerator(this);
        }

        public class CompositeEnumerator : IEnumerator<DomainElement>
        {
            private CompositeDomain _compositeDomain;
            private readonly IEnumerator<DomainElement>[] _iterators;
            private readonly int _size;

            public CompositeEnumerator(CompositeDomain compositeDomain)
            {
                _compositeDomain = compositeDomain;

                _size = _compositeDomain.GetNumberOfComponents();
                _iterators = new IEnumerator<DomainElement>[_size];

                Reset();
            }

            public void Dispose()
            {
                _compositeDomain = null;
            }

            public bool MoveNext()
            {
                int index = _size - 1;
                while (index >= 0)
                {   
                    if (_iterators[index].MoveNext()) return true;
                    _iterators[index].Reset();
                    _iterators[index].MoveNext();
                    index--;
                }
                return false;
            }

            public void Reset()
            {
                for (int i = 0; i < _size; ++i)
                {
                    _iterators[i] = _compositeDomain[i].GetEnumerator();
                    if (i != _size - 1) _iterators[i].MoveNext();
                }
            }

            public DomainElement Current
            {
                get
                {
                    int[] values = new int[_size];

                    for (int i = 0; i < _size; ++i)
                    {
                        var domainElement = _iterators[i].Current;
                        if (domainElement != null) values[i] = domainElement[0];
                    }

                    return new DomainElement(values);
                }
            }

            object IEnumerator.Current => Current;
        }

    }
}
