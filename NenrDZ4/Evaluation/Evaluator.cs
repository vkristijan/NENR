using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using NenrDZ4.Chromosomes;

namespace NenrDZ4.Evaluation
{
    class Evaluator : IEvaluator
    {
        private readonly List<Data> _data;

        public Evaluator(string path)
        {
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
            foreach (var data in _data)
            {
                double x = data.X;
                double y = data.Y;
                double value = Math.Sin(c[0] + c[1] * x) + c[2] * Math.Cos(x * (c[3] + y)) * 1 / (1 + Math.Exp(Math.Pow(x - c[4], 2)));

                error += Math.Pow(value - data.Value, 2);
            }

            c.Cost = error;
            return error;
        }
    }

    class Data
    {
        public Data(string line)
        {
            string[] data = line.Split(new []{ '\t', ' '}, 3);
            X = double.Parse(data[0]);
            Y = double.Parse(data[1]);
            Value = double.Parse(data[2]);
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Value { get; set; }
    }
}
