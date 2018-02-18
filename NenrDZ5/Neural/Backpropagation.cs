using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ5.Neural
{
    class Backpropagation
    {
        private FFANN _ffann;
        private Layer[] _layers;
        private Dataset _dataset;

        private double _learningRate;
        public int MaxIteration { get; set; } = 10000;
        public double MaxError { get; set; } = 1e-6;

        public Backpropagation(FFANN ffann, double learningRate, Dataset dataset)
        {
            _ffann = ffann;
            _layers = ffann.Layers();
            _learningRate = learningRate;
            _dataset = dataset;
        }

        public void Train(int batchSize)
        {
            int iteration = 0;
            double error = _ffann.CalculateError(_dataset);

            while (iteration < MaxIteration && error > MaxError)
            {
                iteration++;
                _dataset.Shuffle();
                Propagate(batchSize);
                error = _ffann.CalculateError(_dataset);

                if (iteration % 10 == 0)
                {
                    Console.WriteLine("iter: #" + iteration + "   -   " + error.ToString("0.000000"));
                   
                }
            }
        }

        private void Propagate(int batchSize)
        {
            int size = _dataset.Size;
            int l = _layers.Length;

            for (int batch = 0; batch < size; batch += batchSize)
            {
                double[][] delta = new double[batchSize][];
                double[][][] y = new double[l][][];
                for (int i = 0; i < l; ++i) y[i] = new double[batchSize][];

                for (int s = 0; s < batchSize && s + batch < size; ++s)
                {
                    y[l - 1][s] = _ffann.GetOutput(_dataset.GetInput(batch + s));
                    for (int k = 0; k < l - 1; ++k) y[k][s] = _layers[k].Values;
                    double[] t = _dataset.GetOutput(batch + s);
                       
                    delta[s] = new double[y[l-1][s].Length];
                    for (int i = 0; i < y[l-1][s].Length; ++i)
                    {
                        delta[s][i] = y[l-1][s][i] * (1 - y[l-1][s][i]) * (t[i] - y[l-1][s][i]);
                    }
                }

                double[][] weights = _layers[l - 1].WeightMatrix();
                int n1 = _layers[l - 2].Size + 1;
                int n2 = _layers[l - 1].Size;

                for (int i = 0; i < n1; ++i)
                {
                    for (int j = 0; j < n2; ++j)
                    {
                        double correction = 0;
                        for (int s = 0; s < batchSize && s + batch < size; ++s)
                        {
                            double yy = i > 0 ? y[l - 2][s][i - 1] : 1;
                            correction += delta[s][j] * yy;
                        }
                        weights[j][i] += _learningRate * correction;
                    }
                }


                //backpropagation for hidden layers
                for (int k = l - 2; k > 0; --k)
                {
                    var oldDelta = delta;
                    delta = new double[batchSize][];

                    for (int s = 0; s < batchSize && s + batch < size; ++s)
                    {
                        delta[s] = new double[y[k][s].Length];
                        for (int j = 0; j < y[k][s].Length; ++j)
                        {
                            delta[s][j] = 0;
                            for (int o = 0; o < y[k + 1][s].Length; ++o)
                            {
                                delta[s][j] += oldDelta[s][o] * weights[o][j];
                            }
                            delta[s][j] *= y[k][s][j] * (1 - y[k][s][j]);
                        }
                    }

                    weights = _layers[k].WeightMatrix();
                    n1 = _layers[k - 1].Size + 1;
                    n2 = _layers[k].Size;

                    for (int i = 0; i < n1; ++i)
                    {
                        for (int j = 0; j < n2; ++j)
                        {
                            double correction = 0;
                            for (int s = 0; s < batchSize && s + batch < size; ++s)
                            {
                                double yy = i > 0 ? y[k-1][s][i - 1] : 1;
                                correction += delta[s][j] * yy;
                            }
                            weights[j][i] += _learningRate * correction;
                        }
                    }
                }

            }
        }
    }
}
