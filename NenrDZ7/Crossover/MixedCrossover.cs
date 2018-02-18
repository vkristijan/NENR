using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NenrDZ7.Chromosomes;

namespace NenrDZ7.Crossover
{
    class MixedCrossover : ICrossover
    {
        private readonly List<ICrossover> _crossovers;

        public MixedCrossover(List<ICrossover> crossovers)
        {
            _crossovers = crossovers;
        }

        public MixedCrossover(params ICrossover[] crossovers)
        {
            _crossovers = crossovers.ToList();
        }

        public Chromosome Crossover(Chromosome a, Chromosome b)
        {
            /*int n = a.Size;
            double[] values = new double[n];

            for (int i = 0; i < n; ++i)
            {
                values[i] = a[i];
            }

            return new Chromosome(values);/**/
            int x = Utility.NextInt(_crossovers.Count);

            return _crossovers[x].Crossover(a, b);/**/
        }
    }
}
