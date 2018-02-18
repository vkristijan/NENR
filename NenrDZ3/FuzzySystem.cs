using NenrDZ1.Fuzzy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ3
{
    class FuzzySystem
    {
        public BinaryFunction sNorm = Operations.ZadehOr();
        public BinaryFunction tNorm = Operations.ZadehAnd();
        public BinaryFunction implication = Operations.ZadehAnd();//Operations.Product();

        protected Defuzzifier defuzzifier;
        protected List<Rule> rules;

        public int Solve(int[] inputs, bool debug = false)
        {
            IFuzzySet solution = rules[0].Accept(inputs);

            for (int i = 1; i < rules.Count; ++i)
            {
                IFuzzySet tmp = rules[i].Accept(inputs);
                solution = Operations.BinaryOperation(solution, tmp, sNorm);
            }

            if (debug)
            {
                Console.WriteLine(solution);
            }
            return defuzzifier.Defuzzify(solution);
        }

        public int Solve(int[] inputs, int pravilo)
        {
            IFuzzySet solution = rules[pravilo].Accept(inputs);
            Console.WriteLine(solution);

            return defuzzifier.Defuzzify(solution);
        }
    }
}
