using System;
using NenrDZ1.Domains;
using NenrDZ1.Fuzzy;

namespace NenrDZ1
{
    internal class OperationsDemo
    {
        private static void Main()
        {
            var d = Domain.IntRange(0, 11); // {0,1,...,10} 

            IFuzzySet set1 = new MutableFuzzySet(d)
                .Set(DomainElement.Of(0), 0.4)
                .Set(DomainElement.Of(1), 0.8)
                .Set(DomainElement.Of(2), 0.6)
                .Set(DomainElement.Of(3), 0.4)
                .Set(DomainElement.Of(4), 0.2);
            Console.WriteLine("Set 1:");
            Console.WriteLine(set1);
            Console.WriteLine();

            var notSet1 = Operations.UnaryOperation(set1, Operations.ZadehNot());
            Console.WriteLine("notSet 1:");
            Console.WriteLine(notSet1);
            Console.WriteLine();

            var union = Operations.BinaryOperation(set1, notSet1, Operations.ZadehOr());
            Console.WriteLine("Set1 union notSet1:");
            Console.WriteLine(union);
            Console.WriteLine();

            var hinters = Operations.BinaryOperation(set1, notSet1, Operations.HamacherTNorm(1.0));
            Console.WriteLine("Set1 intersection with notSet1 using parameterised Hamacher T norm with parameter 1.0:");
            Console.WriteLine(hinters);
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
