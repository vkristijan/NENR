using NenrDZ1.Domains;
using NenrDZ1.Fuzzy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ3
{
    class Rule
    {
        private IFuzzySet[] antecedent;
        private IFuzzySet consequent;

        private BinaryFunction tNorm = Operations.ZadehAnd();
        private BinaryFunction implication = Operations.ZadehAnd();

        public Rule(IFuzzySet[] antecedent, IFuzzySet consequent)
        {
            this.antecedent = antecedent;
            this.consequent = consequent;
        }

        public Rule(IFuzzySet[] antecedent, IFuzzySet consequent, BinaryFunction tNorm, BinaryFunction implication)
        {
            this.antecedent = antecedent;
            this.consequent = consequent;

            this.tNorm = tNorm;
            this.implication = implication;
        }

        public IFuzzySet Accept(int[] inputs)
        {
            IDomain domain = consequent.GetDomain();
            MutableFuzzySet solution = new MutableFuzzySet(domain);

            foreach (var element in domain)
            {
                //Console.Error.WriteLine(antecedent[1]);
                //Console.Error.WriteLine(inputs[0]);
                double tmp = antecedent[0].GetValueAt(DomainElement.Of(inputs[0]));

                for (int i = 1; i < antecedent.Length; ++i)
                {
                    tmp = tNorm(tmp, antecedent[i].GetValueAt(DomainElement.Of(inputs[i])));
                }
                solution.Set(element, implication(tmp, consequent.GetValueAt(element)));
            }

            return solution;
        }
    }
}
