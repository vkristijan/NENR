using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NenrDZ4.Chromosomes;

namespace NenrDZ4.Mutation
{
    class SingleGausMutation : IMutation
    {
        private readonly double _sigma;

        public SingleGausMutation(double sigma)
        {
            _sigma = sigma;
        }

        public void Mutate(Chromosome c)
        {
            int n = c.Size;
            int x = Utility.NextInt(n);
            c[x] += Utility.NextGaussian() * _sigma;
        }
    }
}
