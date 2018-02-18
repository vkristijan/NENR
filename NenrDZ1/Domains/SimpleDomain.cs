using System;
using System.Collections;
using System.Collections.Generic;

namespace NenrDZ1.Domains
{
    public class SimpleDomain : Domain
    {
        public int First { get; set; }
        public int Second { get; set; }

        public SimpleDomain(int first, int second)
        {
            if (first >= second)
            {
                throw new ArgumentException("The first element is expected to be smaller!");
            }
            First = first;
            Second = second;
        }

        public override int GetCardinality()
        {
            return Second - First;
        }

        public override IDomain GetComponent(int index)
        {
            if (index != 0)
            {
                throw new IndexOutOfRangeException();
            }

            return this;
        }

        public override int GetNumberOfComponents()
        {
            return 1;
        }

        public override int IndexOfElement(DomainElement element)
        {
            int value = element[0];
            if (value < First || value >= Second || element.GetNumberOfComponents() > 1)
            {
                throw new Exception("Element not in domain!");
            }

            return value - First;
        }

        public override DomainElement ElementForIndex(int index)
        {
            if (index >= GetCardinality() || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            return DomainElement.Of(First + index);
        }

        public override IEnumerator<DomainElement> GetEnumerator()
        {
            return new SimpleEnumerator(this);
        }

        public class SimpleEnumerator : IEnumerator<DomainElement>
        {
            private SimpleDomain _domain;
            private int _index;
            public SimpleEnumerator(SimpleDomain simpleDomain)
            {
                _domain = simpleDomain;
                _index = simpleDomain.First - 1;
            }

            public void Dispose()
            {
                _domain = null;
            }

            public bool MoveNext()
            {
                _index++;
                return _index < _domain.Second;
            }

            public void Reset()
            {
                _index = _domain.First - 1;
            }

            public DomainElement Current => new DomainElement(new []{_index});

            object IEnumerator.Current => Current;
        }
    }
}
