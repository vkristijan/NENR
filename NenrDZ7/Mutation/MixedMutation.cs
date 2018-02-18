using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NenrDZ7.Chromosomes;

namespace NenrDZ7.Mutation
{
    class MixedMutation : IMutation
    {
        private readonly IMutation _m1;
        private readonly IMutation _m2;
        private double _p;

        public MixedMutation(IMutation m1, IMutation m2, double p)
        {
            _m1 = m1;
            _m2 = m2;
            _p = p;
        }

        public void Mutate(Chromosome c)
        {
            if (Utility.NextDouble() < _p)
            {
                _m1.Mutate(c);
            }
            else
            {
                _m2.Mutate(c);
            }
        }
    }
}
