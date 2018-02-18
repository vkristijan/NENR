using NenrDZ1.Fuzzy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ3
{
    public interface Defuzzifier
    {
        int Defuzzify(IFuzzySet set);
    }

    public class COADefuzzifier : Defuzzifier
    {
        public int Defuzzify(IFuzzySet set)
        {
            double numerator = 0;
            double denominator = 0;
            foreach (var element in set.GetDomain()){
                numerator += element[0] * set.GetValueAt(element);
                denominator += set.GetValueAt(element);
            }

            if (denominator == 0)
            {
                return 0;
            }
            return (int)(numerator / denominator);
        }
    }
}
