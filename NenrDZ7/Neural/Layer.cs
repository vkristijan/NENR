using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ7.Neural
{
    public class Layer : ILayer
    {
        private static readonly double[] Bias = {1.0};

        private readonly ActivationFunction _activationFunction;
        private double[][] _weights;

        protected override double[] getWeights()
        {
            int n1 = Previous.Size + 1;
            int n2 = Values.Length;

            double[] weights = new double[n1 * n2];
            int counter = 0;
            for (int i = 0; i < n2; ++i)
            {
                for (int j = 0; j < n1; ++j)
                {
                    weights[counter] = _weights[i][j];
                    counter++;
                }
            }
            return weights;
        }

        protected override void setWeights(double[] value)
        {
            if (Previous == null) return;
            int n1 = Previous.Size + 1;
            int n2 = Values.Length;

            double[][] weights = new double[n2][];
            int counter = 0;
            for (int i = 0; i < n2; ++i)
            {
                weights[i] = new double[n1];
                for (int j = 0; j < n1; ++j)
                {
                    weights[i][j] = value[counter];
                    counter++;
                }
            }
            _weights = weights;
        }

        public Layer(int size, ActivationFunction activationFunction)
        {
            _activationFunction = activationFunction;

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

            double[] input = Bias.Concat(Previous.Values).ToArray();

            int n1 = input.Length;
            int n2 = Values.Length;
            for (int i = 0; i < n2; ++i)
            {
                Values[i] = 0;
                for (int j = 0; j < n1; ++j)
                {
                    Values[i] += _weights[i][j] * input[j];
                }
                Values[i] = _activationFunction(Values[i]);
            }
        }

        public override int WeightCount()
        {
            if (Previous == null) return 0;

            int n1 = Previous.Size + 1;
            int n2 = Values.Length;

            return n1 * n2;
        }

        public double[][] WeightMatrix() => _weights;
    }
}
