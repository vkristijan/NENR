using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ7.Neural
{
    public class FFANN
    {
        private readonly ILayer[] _layers;
        public ILayer[] Layers() => _layers;

        public FFANN(int[] layout, ActivationFunction[] activationFunctions)
        {
            int n = layout.Length;
            _layers = new ILayer[n];

            _layers[0] = new Layer(layout[0], ActivationFunctions.Linear());
            _layers[1] = new Type1Layer(layout[1]) {Previous = _layers[0]};

            for (int i = 2; i < n; ++i)
            {
                _layers[i] = new Layer(layout[i], activationFunctions[i - 2])
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

        public double[] GetFinalOutput(double[] input)
        {
            double[] output = GetOutput(input);

            /*double m = output[0];
            int index = 0;

            for (int i = 1; i < output.Length; ++i)
            {
                if (output[i] > m)
                {
                    m = output[i];
                    index = i;
                }
                output[i] = 0;
            }
            output[index] = 1;

            /**/for (int i = 0; i < output.Length; ++i)
            {
                if (output[i] > 0.5)
                {
                    output[i] = 1;
                }
                else
                {
                    output[i] = 0;
                }
            }/**/
            return output;
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
                index += n;

                layer.Weights = layerWeights;
            }
        }

    }
}
