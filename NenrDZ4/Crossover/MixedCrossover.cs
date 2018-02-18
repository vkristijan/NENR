using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NenrDZ4.Chromosomes;

namespace NenrDZ4.Crossover
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
            int x = Utility.NextInt(_crossovers.Count);

            return _crossovers[x].Crossover(a, b);
        }
    }
}
