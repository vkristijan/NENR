using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NenrDZ7.Chromosomes
{
    public class Chromosome
    {
        public int Size { get; set; }
        public double Cost { get; set; }

        public double[] _values;

        public Chromosome(int size)
        {
            Size = size;
            _values = new double[Size];

            for (int i = 0; i < Size; ++i)
            {
                _values[i] = 0.5 + Utility.NextGaussian()*0.1;
            }
        }

        public Chromosome(double[] values)
        {
            Size = values.Length;
            _values = new double[Size];
            values.CopyTo(_values, 0);
        }

        public double this[int i]
        {
            get => _values[i];
            set => _values[i] = value;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");
            foreach (var value in _values)
            {
                sb.Append(value.ToString("0.00000"))
                  .Append(" ");
            }
            sb.Append("]");

            sb.Append(" - ");
            sb.Append(Cost.ToString("0.0000"));
            return sb.ToString();
        }
    }
}
