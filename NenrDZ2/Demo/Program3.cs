using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NenrDZ1.Domains;
using NenrDZ1.Fuzzy;

namespace NenrDZ2
{
    class Program3
    {
        static void Main(string[] args)
        {
            IDomain u = Domain.IntRange(1, 5);  // {1,2,3,4}

            IFuzzySet r = new MutableFuzzySet(Domain.Combine(u, u))
                .Set(DomainElement.Of(1, 1), 1)
                .Set(DomainElement.Of(2, 2), 1)
                .Set(DomainElement.Of(3, 3), 1)
                .Set(DomainElement.Of(4, 4), 1)
                .Set(DomainElement.Of(1, 2), 0.3)
                .Set(DomainElement.Of(2, 1), 0.3)
                .Set(DomainElement.Of(2, 3), 0.5)
                .Set(DomainElement.Of(3, 2), 0.5)
                .Set(DomainElement.Of(3, 4), 0.2)
                .Set(DomainElement.Of(4, 3), 0.2);

            IFuzzySet r2 = r;
            Console.Write("Početna relacija je neizrazita relacija ekvivalencije? ");
            Console.WriteLine(Relations.IsFuzzyEquivalence(r2));
            Console.WriteLine();

            for (int i = 1; i <= 3; i++)
            {
                r2 = Relations.CompositionOfBinaryRelations(r2, r);
                Console.WriteLine("Broj odrađenih kompozicija: " + i + ". Relacija je:");
                Console.Write(r2);

                Console.Write("Ova relacija je neizrazita relacija ekvivalencije? ");
                Console.WriteLine(Relations.IsFuzzyEquivalence(r2));
                Console.WriteLine();
            }


            Console.ReadKey();
        }
    }
}
