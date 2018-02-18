using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NenrDZ7.Chromosomes;

namespace NenrDZ7.Mutation
{
    class NewGausMutation : IMutation
    {
        private readonly double _sigma;
        private readonly double _p;

        public NewGausMutation(double sigma, double p)
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
                    c[i] = 0.5 + Utility.NextGaussian() * _sigma;
                }
            }
        }
    }
}
