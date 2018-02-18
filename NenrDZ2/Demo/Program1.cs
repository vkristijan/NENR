using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NenrDZ1.Domains;
using NenrDZ1.Fuzzy;

namespace NenrDZ2
{
    class Program1
    {
        static void Main(string[] args)
        {
            IDomain u = Domain.IntRange(1, 6);
            IDomain u2 = Domain.Combine(u, u);

            IFuzzySet r1 = new MutableFuzzySet(u2)
                .Set(DomainElement.Of(1, 1), 1)
                .Set(DomainElement.Of(2, 2), 1)
                .Set(DomainElement.Of(3, 3), 1)
                .Set(DomainElement.Of(4, 4), 1)
                .Set(DomainElement.Of(5, 5), 1)
                .Set(DomainElement.Of(3, 1), 0.6)
                .Set(DomainElement.Of(1, 3), 0.5);

            IFuzzySet r2 = new MutableFuzzySet(u2)
                .Set(DomainElement.Of(1, 1), 1)
                .Set(DomainElement.Of(2, 2), 1)
                .Set(DomainElement.Of(3, 3), 1)
                .Set(DomainElement.Of(4, 4), 1)
                .Set(DomainElement.Of(5, 5), 1)
                .Set(DomainElement.Of(3, 1), 0.5)
                .Set(DomainElement.Of(1, 3), 0.1);

            IFuzzySet r3 = new MutableFuzzySet(u2)
                .Set(DomainElement.Of(1, 1), 1)
                .Set(DomainElement.Of(2, 2), 1)
                .Set(DomainElement.Of(3, 3), 0.3)
                .Set(DomainElement.Of(4, 4), 1)
                .Set(DomainElement.Of(5, 5), 1)
                .Set(DomainElement.Of(1, 2), 0.6)
                .Set(DomainElement.Of(2, 1), 0.6)
                .Set(DomainElement.Of(2, 3), 0.7)
                .Set(DomainElement.Of(3, 2), 0.7)
                .Set(DomainElement.Of(3, 1), 0.5)
                .Set(DomainElement.Of(1, 3), 0.5);

            IFuzzySet r4 = new MutableFuzzySet(u2)
                .Set(DomainElement.Of(1, 1), 1)
                .Set(DomainElement.Of(2, 2), 1)
                .Set(DomainElement.Of(3, 3), 1)
                .Set(DomainElement.Of(4, 4), 1)
                .Set(DomainElement.Of(5, 5), 1)
                .Set(DomainElement.Of(1, 2), 0.4)
                .Set(DomainElement.Of(2, 1), 0.4)
                .Set(DomainElement.Of(2, 3), 0.5)
                .Set(DomainElement.Of(3, 2), 0.5)
                .Set(DomainElement.Of(1, 3), 0.4)
                .Set(DomainElement.Of(3, 1), 0.4);

            bool test1 = Relations.IsUTimesURelation(r1);
            Console.WriteLine("r1 je definiran nad UxU? " + test1);

            bool test2 = Relations.IsSymmetric(r1);
            Console.WriteLine("r1 je simetrična? " + test2);

            bool test3 = Relations.IsSymmetric(r2);
            Console.WriteLine("r2 je simetrična? " + test3);

            bool test4 = Relations.IsReflexive(r1);
            Console.WriteLine("r1 je refleksivna? " + test4);

            bool test5 = Relations.IsReflexive(r3);
            Console.WriteLine("r3 je refleksivna? " + test5);

            bool test6 = Relations.IsMaxMinTransitive(r3);
            Console.WriteLine("r3 je max-min tranzitivna? " + test6);

            bool test7 = Relations.IsMaxMinTransitive(r4);
            Console.WriteLine("r4 je max-min tranzitivna? " + test7);


            Console.ReadKey();
        }
    }
}
