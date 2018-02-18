using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ7.Neural
{
    public class Type1Layer : ILayer
    {
        private double[][] _w;
        private double[][] _s;

        protected override double[] getWeights()
        {
            int n1 = Previous.Size;
            int n2 = Values.Length;

            double[] weights = new double[2 * n1 * n2];
            int counter = 0;
            for (int i = 0; i < n2; ++i)
            {
                for (int j = 0; j < n1; ++j)
                {
                    weights[counter] = _w[i][j];
                    counter++;
                    weights[counter] = _s[i][j];
                    counter++;
                }
            }
            return weights;
        }

        protected override void setWeights(double[] value)
        {
            if (Previous == null) return;
            int n1 = Previous.Size;
            int n2 = Values.Length;

            _w = new double[n2][];
            _s = new double[n2][];
            int counter = 0;
            for (int i = 0; i < n2; ++i)
            {
                _w[i] = new double[n1];
                _s[i] = new double[n1];
                for (int j = 0; j < n1; ++j)
                {
                    _w[i][j] = value[counter];
                    counter++;
                    _s[i][j] = value[counter];
                    counter++;
                }
            }
        }

        public Type1Layer(int size)
        {
            Values = new double[size];
        }

        

        public override void SetInput(double[] input)
        {
            if (_previous != null) throw new Exception("Unable to set input on non-input layer.");
            if (Values.Length != input.Length) throw new Exception("Invalid number of inputs.");

            input.CopyTo(Values, 0);
        }

        public override void CalculateValues()
        {
            if (Previous == null) return;

            double[] input = Previous.Values;

            int n1 = input.Length;
            int n2 = Values.Length;
            for (int i = 0; i < n2; ++i)
            {
                Values[i] = 1;
                for (int j = 0; j < n1; ++j)
                {
                    Values[i] += (Math.Abs(input[j] - _w[i][j]) / Math.Abs(_s[i][j]));
                }
                Values[i] = 1 / Values[i];
            }
        }

        public override int WeightCount()
        {
            if (Previous == null) return 0;

            int n1 = Previous.Size;
            int n2 = Values.Length;

            return 2 * n1 * n2;
        }
    }
}
