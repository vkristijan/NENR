using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ5.Neural
{
    public delegate double ActivationFunction(double x);

    public static class ActivationFunctions
    {
        public static ActivationFunction Step() => x => x > 0 ? 1 : 0;
        public static ActivationFunction Linear() => x => x;
        public static ActivationFunction Sigmoid() => x => 1 / (1 + Math.Exp(-x));
    }
}
