using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NenrDZ7.Chromosomes;

namespace NenrDZ7.Mutation
{
    class GausMutation : IMutation
    {
        private readonly double _sigma;
        private readonly double _p;

        public GausMutation(double sigma, double p)
        {
            _sigma = sigma;
            _p = p;
        }

        public void Mutate(Chromosome c)
        {
            for (int i = 0; i < c.Size; ++i)
            {
                if (Utility.NextDouble() < _p)
                {
                    c[i] += Utility.NextGaussian() * _sigma;

                }
            }
        }
    }
}
