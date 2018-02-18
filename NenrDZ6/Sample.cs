using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ6
{
    class Sample
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Sample(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static List<Sample> GenerateTrainingSet()
        {
            var samples = new List<Sample>();

            for (int x = -4; x <= 4; ++x)
            {
                for (int y = -4; y <= 4; ++y)
                {
                    samples.Add(new Sample(x, y, _f(x, y)));
                }
            }

            return samples;
        }

        private static double _f(double x, double y) 
            => (Math.Pow(x - 1, 2) + Math.Pow(y + 2, 2) - 5 * x * y + 3) * Math.Pow(Math.Cos(x/5), 2);
    }
}
