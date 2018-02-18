using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ6
{
    class Membership
    {
        public static double Sigmoid(double x, double a, double b) => 1 / (1 + Math.Exp(b * (x - a)));
    }
}
