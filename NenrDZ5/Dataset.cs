using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace NenrDZ5
{
    public class Dataset
    {
        protected List<double[]> _input;
        protected List<double[]> _output;

        public Dataset()
        {
            _input = new List<double[]>();
            _output = new List<double[]>();
        }

        public int Size => _input.Count;

        public virtual double[] GetInput(int index) => _input[index];
        public double[] GetOutput(int index) => _output[index];

        public void Add(List<PointF> line, Label label)
        {
            int n = line.Count;
            double[] values = new double[2 * n];

            for (int i = 0; i < n; ++i)
            {
                values[2 * i] = line[i].X;
                values[2 * i + 1] = line[i].Y;
            }

            _input.Add(values);
            _output.Add(Encoder.Encode(label));
        }

        public void Save(string fileName)
        {
            int n = _input.Count;
            var lines = new List<string>(n);

            for (int i = 0; i < n; ++i)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                foreach (var x in _input[i])
                {
                    sb.Append(x);
                    sb.Append(" ");
                }
                sb.Append("] - [");
                foreach (var x in _output[i])
                {
                    sb.Append(x);
                    sb.Append(" ");
                }
                sb.Append("]");

                lines.Add(sb.ToString());
            }

            File.WriteAllLines(fileName, lines);
        }

        public void Read(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            int n = lines.Length;

            _input = new List<double[]>(n);
            _output = new List<double[]>(n);

            foreach (var line in lines)
            {
                var lineParts = line.Split(new[] { " - " }, StringSplitOptions.None);

                _input.Add(ParseList(lineParts[0]));
                _output.Add(ParseList(lineParts[1]));
            }
        }

        private double[] ParseList(string line)
        {
            line = line.Trim(' ', '[', ']');
            var parts = line.Split(' ');
            int n = parts.Length;
            double[] values = new double[n];

            for (int i = 0; i < n; ++i)
            {
                values[i] = Double.Parse(parts[i].Trim());
            }

            return values;
        }

        public void Shuffle()
        {
            List<int> indexList = Enumerable.Range(0, Size).ToList();
            indexList.Shuffle();

            List<double[]> input = new List<double[]>(Size);
            List<double[]> output = new List<double[]>(Size);

            foreach (var i in indexList)
            {
                input.Add(_input[i]);
                output.Add(_output[i]);
            }

            _input = input;
            _output = output;
        }
    }

    public class PointDataset : Dataset
    {
        private readonly int _numberOfPoints;

        public PointDataset(int numberOfPoints)
        {
            _numberOfPoints = numberOfPoints;
        }

        public override double[] GetInput(int index) => ScalePoints(_input[index]);

        private double[] ScalePoints(double[] points)
        {
            int n = _numberOfPoints;
            double[] result = new double[2 * n];

            int k = points.Length / 2;
            float length = 0;
            for (int i = 1; i < k; ++i)
            {
                length += Distance(points[i * 2], points[i * 2 + 1],
                    points[(i - 1) * 2], points[(i - 1) * 2 + 1]);
            }

            float tmpLength = 0;
            float desiredLength = 0;
            int index = 1;
            for (int i = 0; i < n; ++i)
            {
                float d = Distance(points[index * 2], points[index * 2 + 1],
                    points[(index - 1) * 2], points[(index - 1) * 2 + 1]);
                while (index < k - 1 && tmpLength + d < desiredLength)
                {
                    tmpLength += d;
                    index++;
                    d = Distance(points[index * 2], points[index * 2 + 1],
                        points[(index - 1) * 2], points[(index - 1) * 2 + 1]);
                }

                float alpha = (desiredLength - tmpLength) / d;

                double ax = points[(index - 1) * 2];
                double ay = points[(index - 1) * 2 + 1];
                double bx = points[index * 2];
                double by = points[index * 2 + 1];

                result[i * 2] = ax + alpha * (bx - ax);
                result[i * 2 + 1] = ay + alpha * (by - ay);

                desiredLength += length / (n - 1);
            }

            return result;
        }

        private float Distance(double ax, double ay, double bx, double by)
            => (float)Math.Sqrt(Math.Pow(ax - bx, 2) + Math.Pow(ay - by, 2));
    }
}
