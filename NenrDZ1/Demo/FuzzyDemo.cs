using System;
using NenrDZ1.Domains;
using NenrDZ1.Fuzzy;

namespace NenrDZ1
{
    internal class FuzzyDemo
    {
        private static void Main()
        {
            var d = Domain.IntRange(0, 11); // {0,1,...,10} 

            IFuzzySet set1 = new MutableFuzzySet(d)
                .Set(DomainElement.Of(0), 1.0)
                .Set(DomainElement.Of(1), 0.8)
                .Set(DomainElement.Of(2), 0.6)
                .Set(DomainElement.Of(3), 0.4)
                .Set(DomainElement.Of(4), 0.2);
            Console.WriteLine("Set 1:");
            Console.WriteLine(set1);
            Console.WriteLine();

            var d2 = Domain.IntRange(-5, 6); // {-5,-4,...,4,5} 
            IFuzzySet set2 = new CalculatedFuzzySet(
                d2,
                StandardFuzzySets.LambdaFunction(
                    d2.IndexOfElement(DomainElement.Of(-4)),
                    d2.IndexOfElement(DomainElement.Of(0)), 
                    d2.IndexOfElement(DomainElement.Of(4))
                )
            );
            Console.WriteLine("Set 2:");
            Console.WriteLine(set2);
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
