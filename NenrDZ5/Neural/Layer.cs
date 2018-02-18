using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ5.Neural
{
    public class Layer
    {
        private static readonly double[] Bias = {1.0};

        private Layer _previous;
        public Layer Previous 
        {
            private get => _previous;
            set
            {
                _previous = value;
                value._next = this;
            }
        }

        private Layer _next;
        public Layer Next
        {
            private get => _next;
            set
            {
                _next = value;
                value._previous = this;
            }
        }

        private readonly ActivationFunction _activationFunction;
        private double[][] _weights;

        public double[] Weights
        {
            get {
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
            set
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
        }

        public double[] Values { get; }

        public Layer(int size, ActivationFunction activationFunction)
        {
            _activationFunction = activationFunction;

            Values = new double[size];
        }

        public int Size => Values.Length;

        public void SetInput(double[] input)
        {
            if (_previous != null) throw new Exception("Unable to set input on non-input layer.");
            if (Values.Length != input.Length) throw new Exception("Invalid number of inputs.");

            input.CopyTo(Values, 0);
        }

        public void CalculateValues()
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

        public int WeightCount()
        {
            if (Previous == null) return 0;

            int n1 = Previous.Size + 1;
            int n2 = Values.Length;

            return n1 * n2;
        }

        public double[][] WeightMatrix() => _weights;
    }
}
