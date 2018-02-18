using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NenrDZ4.Chromosomes;

namespace NenrDZ4.Selection
{
    class TournamentSelection : ISelection
    {
        private readonly int _size;

        public TournamentSelection(int size)
        {
            _size = size;
        }

        public (Chromosome, Chromosome) Select(List<Chromosome> population)
        {
            return Select(population, out var c);
        }

        public (Chromosome, Chromosome) Select(List<Chromosome> population, out Chromosome worst)
        {
            var selected = new List<Chromosome>();
            for (int i = 0; i < _size; ++i)
            {
                int index = Utility.NextInt(population.Count);
                selected.Add(population[index]);
            }

            selected = selected.OrderBy(x => x.Cost).ToList();
            worst = selected[_size - 1];
            return (selected[0], selected[1]);
        }
    }
}
