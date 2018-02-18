using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NenrDZ7.Chromosomes;

namespace NenrDZ7.Crossover
{
    interface ICrossover
    {
        Chromosome Crossover(Chromosome a, Chromosome b);
    }
}
