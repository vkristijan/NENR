using System.Collections.Generic;

namespace NenrDZ1.Domains
{
    public interface IDomain : IEnumerable<DomainElement>
    {
        int GetCardinality();
        IDomain GetComponent(int index);
        int GetNumberOfComponents();
        int IndexOfElement(DomainElement element);
        bool HasElement(DomainElement element);
        DomainElement ElementForIndex(int index);
        IDomain this[int i] { get; }
    }
}
