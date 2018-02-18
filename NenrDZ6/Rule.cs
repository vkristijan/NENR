using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ6
{
    class Rule
    {
        private static Random rnd = new Random();

        private double[] _parameters;
        public double this[int i]
        {
            get => _parameters[i];
            set => _parameters[i] = value;
        }

        public Rule()
        {
            _parameters = new double[7];
            for (int i = 0; i < 7; ++i)
            {
                this[i] = rnd.NextDouble() * 10 - 5;
            }
        }

        public double A(double x) => Membership.Sigmoid(x, _parameters[0], _parameters[1]);
        public double B(double y) => Membership.Sigmoid(y, _parameters[2], _parameters[3]);
        public double TNorm(double x, double y) => A(x) * B(y);
        public double Z(double x, double y) => x * _parameters[4] + y * _parameters[5] + _parameters[6];

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var parameter in _parameters)
            {
                sb.Append(parameter.ToString("0.0000"))
                  .Append(" ");
            }
            return sb.ToString();
        }
    }
}
