using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NenrDZ4.Chromosomes;

namespace NenrDZ4.Crossover
{
    class SimpleArithmeticRecombination : ICrossover

    {
        public Chromosome Crossover(Chromosome a, Chromosome b)
        {
            int n = a.Size;
            double[] values = new double[n];

            int x = Utility.NextInt(n);
            bool direction = Utility.NextBool();
            for (int i = 0; i < n; ++i)
            {
                if (i < n && direction || i > n && !direction)
                {
                    values[i] = a[i];
                }
                else 
                {
                    values[i] = (a[i] + b[i]) / 2;
                }
            }

            return new Chromosome(values);
        }
    }
}
