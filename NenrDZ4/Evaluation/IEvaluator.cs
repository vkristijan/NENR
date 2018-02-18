using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NenrDZ4.Chromosomes;

namespace NenrDZ4.Evaluation
{
    interface IEvaluator
    {
        double Evaluate(Chromosome c);
    }
}
