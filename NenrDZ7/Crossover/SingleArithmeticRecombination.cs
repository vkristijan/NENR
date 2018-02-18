using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NenrDZ7.Chromosomes;

namespace NenrDZ7.Crossover
{
    class SingleArithmeticRecombination : ICrossover
    {
        public Chromosome Crossover(Chromosome a, Chromosome b)
        {
            int n = a.Size;
            double[] values = new double[n];
            
            int x = Utility.NextInt(n);
            for (int i = 0; i < n; ++i)
            {
                if (i != x)
                {
                    values[i] = a[i];
                }
                else
                {
                    values[i] = b[i];
                }
            }

            return new Chromosome(values);
        }
    }
}
