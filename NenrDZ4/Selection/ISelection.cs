using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NenrDZ4.Chromosomes;

namespace NenrDZ4.Selection
{
    interface ISelection
    {
        (Chromosome, Chromosome) Select(List<Chromosome> population);
        (Chromosome, Chromosome) Select(List<Chromosome> population, out Chromosome worst);
    }
}
