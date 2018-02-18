using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NenrDZ4.Chromosomes;

namespace NenrDZ4.Mutation
{
    class MixedMutation : IMutation
    {
        private readonly List<IMutation> _mutations;

        public MixedMutation(List<IMutation> mutations)
        {
            _mutations = mutations;
        }

        public MixedMutation(params IMutation[] mutations)
        {
            _mutations = mutations.ToList();
        }

        public void Mutate(Chromosome c)
        {
            int x = Utility.NextInt(_mutations.Count);
            _mutations[x].Mutate(c);
        }
    }
}
