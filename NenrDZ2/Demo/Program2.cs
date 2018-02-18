using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NenrDZ1.Domains;
using NenrDZ1.Fuzzy;

namespace NenrDZ2
{
    class Program2
    {
        static void Main(string[] args)
        {
            IDomain u1 = Domain.IntRange(1, 5);  // {1,2,3,4} 
            IDomain u2 = Domain.IntRange(1, 4);  // {1,2,3} 
            IDomain u3 = Domain.IntRange(1, 5);  // {1,2,3,4}

            IFuzzySet r1 = new MutableFuzzySet(Domain.Combine(u1, u2))
                .Set(DomainElement.Of(1, 1), 0.3)
                .Set(DomainElement.Of(1, 2), 1)
                .Set(DomainElement.Of(3, 3), 0.5)
                .Set(DomainElement.Of(4, 3), 0.5);

            IFuzzySet r2 = new MutableFuzzySet(Domain.Combine(u2, u3))
                .Set(DomainElement.Of(1, 1), 1)
                .Set(DomainElement.Of(2, 1), 0.5)
                .Set(DomainElement.Of(2, 2), 0.7)
                .Set(DomainElement.Of(3, 3), 1)
                .Set(DomainElement.Of(3, 4), 0.4);

            IFuzzySet r1r2 = Relations.CompositionOfBinaryRelations(r1, r2);

            Console.WriteLine(r1r2);

            Console.ReadKey();
        }
    }
}
