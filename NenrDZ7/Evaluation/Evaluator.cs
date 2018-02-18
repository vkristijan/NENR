using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using NenrDZ7.Neural;
using NenrDZ7.Chromosomes;

namespace NenrDZ7.Evaluation
{
    class Evaluator : IEvaluator
    {
        private readonly List<Data> _data;
        private FFANN _ffann;

        public Evaluator(string path, FFANN ffann)
        {
            _ffann = ffann;
            _data = new List<Data>();
            string[] lines = System.IO.File.ReadAllLines(path);

            foreach (var line in lines)
            {
                _data.Add(new Data(line.Trim()));
            }
        }

        public double Evaluate(Chromosome c)
        {
            double error = 0;

            _ffann.SetWeights(c._values);
            foreach (var data in _data)
            {
                double x = data.X;
                double y = data.Y;
                double[] input = {x, y};
                double[] output = _ffann.GetOutput(input);

                error += Math.Pow(output[0] - data.Z1, 2);
                error += Math.Pow(output[1] - data.Z2, 2);
                error += Math.Pow(output[2] - data.Z3, 2);
            }

            error /= 3;
            error /= _data.Count;
            c.Cost = error;
            return error;
        }

        public int FinalError(Chromosome c)
        {
            double tolerance = 0.1;
            int error = 0;
            _ffann.SetWeights(c._values);
            foreach (var data in _data)
            {
                double x = data.X;
                double y = data.Y;
                double[] input = { x, y };
                double[] output = _ffann.GetFinalOutput(input);

                if (Math.Abs(output[0] - data.Z1) > tolerance || 
                    Math.Abs(output[1] - data.Z2) > tolerance || 
                    Math.Abs(output[2] - data.Z3) > tolerance)
                {
                    error++;
                }

                Console.WriteLine(data.Z1 + " " + data.Z2 + " " + data.Z3 + " - " 
                    + output[0] + " " + output[1] + " " + output[2]);
            }
            return error;
        }
    }

    class Data
    {
        public Data(string line)
        {
            string[] data = line.Split(new []{ '\t', ' '}, 5);
            X = double.Parse(data[0]);
            Y = double.Parse(data[1]);
            Z1 = double.Parse(data[2]);
            Z2 = double.Parse(data[3]);
            Z3 = double.Parse(data[4]);
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z1 { get; set; }
        public double Z2 { get; set; }
        public double Z3 { get; set; }

    }
}
