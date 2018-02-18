using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ4
{
    public static class Utility
    {
        private static readonly Random Rnd = new Random();

        public static double NextDouble()
        {
            return Rnd.NextDouble();
        }

        public static double NextDouble(double a, double b)
        {
            return Rnd.NextDouble() * (b - a) + a;
        }

        public static int NextInt(int a)
        {
            return Rnd.Next(a);
        }

        public static double NextGaussian()
        {
            double a = Rnd.NextDouble();
            double b = Rnd.NextDouble();

            return Math.Sqrt(-2 * Math.Log(a)) * Math.Sin(2 * Math.PI * b);
        }

        public static bool NextBool()
        {
            return Rnd.NextDouble() > 0.5;
        }
    }
}
