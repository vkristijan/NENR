using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ5
{
    public enum Label
    {
        Alpha,
        Beta,
        Gamma,
        Delta,
        Epsilon
    }

    public class Encoder
    {
        public static double[] Encode(Label label)
        {
            switch (label)
            {
                case Label.Alpha: return new[] { 1.0, 0, 0, 0, 0 };
                case Label.Beta: return new[] { 0, 1.0, 0, 0, 0 };
                case Label.Gamma: return new[] { 0, 0, 1.0, 0, 0 };
                case Label.Delta: return new[] { 0, 0, 0, 1.0, 0 };
                case Label.Epsilon: return new[] { 0, 0, 0, 0, 1.0 };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static Label Decode(double[] values)
        {
            if (values.Length != 5) throw new ArgumentException();

            double maxValue = values.Max();
            if (Math.Abs(values[0] - maxValue) < Tolerance) return Label.Alpha;
            if (Math.Abs(values[1] - maxValue) < Tolerance) return Label.Beta;
            if (Math.Abs(values[2] - maxValue) < Tolerance) return Label.Gamma;
            if (Math.Abs(values[3] - maxValue) < Tolerance) return Label.Delta;
            return Label.Epsilon;
        }

        public static double Tolerance = 1e-6;

        public static Label LabelFromText(string label)
        {
            switch (label)
            {
                case "Alpha": return Label.Alpha;
                case "Beta": return Label.Beta;
                case "Gamma": return Label.Gamma;
                case "Delta": return Label.Delta;
                case "Epsilon": return Label.Epsilon;
                default: throw new ArgumentException();
            }
        }
    }
}
