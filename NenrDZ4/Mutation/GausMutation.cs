using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NenrDZ4.Chromosomes;

namespace NenrDZ4.Mutation
{
    class GausMutation : IMutation
    {
        private readonly double _sigma;

        public GausMutation(double sigma)
        {
            _sigma = sigma;
        }

        public void Mutate(Chromosome c)
        {
            for (int i = 0; i < c.Size; ++i)
            {
                c[i] += Utility.NextGaussian() * _sigma;
            }
        }
    }
}
