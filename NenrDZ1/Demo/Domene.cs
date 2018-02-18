using System;
using NenrDZ1.Domains;

namespace NenrDZ1
{
    internal class Domene
    {
        private static void Main()
        {
            var d1 = Domain.IntRange(0, 5);  // {0,1,2,3,4} 
            Console.WriteLine("Elementi domene d1:");
            Console.WriteLine(d1);
            Console.WriteLine();

            var d2 = Domain.IntRange(0, 3);  // {0,1,2} 
            Console.WriteLine("Elementi domene d2:");
            Console.WriteLine(d2);
            Console.WriteLine();

            var d3 = Domain.Combine(d1, d2);
            Console.WriteLine("Elementi domene d3:");
            Console.WriteLine(d3);
            Console.WriteLine();

            Console.WriteLine(d3.ElementForIndex(0));
            Console.WriteLine(d3.ElementForIndex(5));
            Console.WriteLine(d3.ElementForIndex(14));
            Console.WriteLine(d3.IndexOfElement(DomainElement.Of(4, 1)));

            Console.ReadKey();
        }
    }
}
