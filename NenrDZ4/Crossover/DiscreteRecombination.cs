using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NenrDZ4.Chromosomes;

namespace NenrDZ4.Crossover
{
    class DiscreteRecombination : ICrossover
    {
        public Chromosome Crossover(Chromosome a, Chromosome b)
        {
            int n = a.Size;
            double[] values = new double[n];

            for (int i = 0; i < n; ++i)
            {
                if (Utility.NextBool())
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
