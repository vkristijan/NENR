using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ5.Neural
{
    public class FFANN
    {
        private readonly Layer[] _layers;
        public Layer[] Layers() => _layers;

        public FFANN(int[] layout, ActivationFunction[] activationFunctions)
        {
            int n = layout.Length;
            _layers = new Layer[n];

            _layers[0] = new Layer(layout[0], ActivationFunctions.Linear());
            for (int i = 1; i < n; ++i)
            {
                _layers[i] = new Layer(layout[i], activationFunctions[i - 1])
                {
                    Previous = _layers[i - 1]
                };
            }
        }

        public int InputSize() => _layers[0].Size;

        public double[] GetOutput(double[] input)
        {
            if (input.Length != _layers[0].Size) throw new ArgumentException("Wrong input size!");

            _layers[0].SetInput(input);

            for (int i = 1; i < _layers.Length; ++i)
            {
                _layers[i].CalculateValues();
            }

            return _layers[_layers.Length - 1].Values;
        }

        public int WeightCount()
        {
            int count = 0;

            foreach (var layer in _layers)
            {
                count += layer.WeightCount();
            }

            return count;
        }

        public double[] GetWeights()
        {
            double[] weights = new double[WeightCount()];

            int index = 0;
            foreach (var layer in _layers)
            {
                if (layer.WeightCount() == 0) continue;

                double[] layerWeights = layer.Weights;
                layerWeights.CopyTo(weights, index);
                index += layerWeights.Length;
            }

            return weights;
        }

        public void SetWeights(double[] weights)
        {
            int index = 0;
            foreach (var layer in _layers)
            {
                int n = layer.WeightCount();
                double[] layerWeights = new double[n];

                for (int i = 0; i < n; ++i)
                {
                    layerWeights[i] = weights[i + index];
                }

                layer.Weights = layerWeights;
            }
        }

        public double CalculateError(Dataset dataset)
        {
            double error = 0;
            int n = dataset.Size;

            for (int i = 0; i < n; ++i)
            {
                double[] output = GetOutput(dataset.GetInput(i));
                double[] expected = dataset.GetOutput(i);

                int k = output.Length;
                if (k != expected.Length) throw new Exception("Output length not as expected!");
                for (int j = 0; j < k; ++j)
                {
                    error += Math.Pow(output[j] - expected[j], 2);
                }
            }

            error /= (2 * n);
            return error;
        }
    }
}
