using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NenrDZ5.Neural;

namespace NenrDZ5
{
    public partial class Recognition : Form
    {
        private FFANN _ffann;

        public Recognition(FFANN ffann)
        {
            InitializeComponent();

            _ffann = ffann;
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            List<PointF> points = canvas.GetPoints();

            int n = _ffann.InputSize() / 2;
            var input = PointFListToDoubleArray(ScalePoints(points, n));
            double[] output = _ffann.GetOutput(input);
            Label label = Encoder.Decode(output);
            lbl_label.Text = label.ToString();
        }

        private double[] PointFListToDoubleArray(List<PointF> points)
        {
            int n = points.Count;
            double[] result = new double[2 * n];

            for (int i = 0; i < n; ++i)
            {
                result[2 * i] = points[i].X;
                result[2 * i + 1] = points[i].Y;
            }

            return result;
        }

        private List<PointF> ScalePoints(List<PointF> points, int n)
        {
            List<PointF> result = new List<PointF>(n);

            float length = 0;
            for (int i = 1; i < points.Count; ++i)
            {
                length += Distance(points[i - 1], points[i]);
            }

            float tmpLength = 0;
            float desiredLength = 0;
            int index = 1;
            for (int i = 0; i < n; ++i)
            {
                float d = Distance(points[index - 1], points[index]);
                while (index < points.Count - 1 && tmpLength + d < desiredLength)
                {
                    tmpLength += d;
                    index++;
                    d = Distance(points[index - 1], points[index]);
                }

                float alpha = (desiredLength - tmpLength) / d;

                PointF a = points[index - 1];
                PointF b = points[index];
                PointF newPoint = new PointF(
                    a.X + alpha * (b.X - a.X), 
                    a.Y + alpha * (b.Y - a.Y)
                );
                result.Add(newPoint);

                desiredLength += length / (n - 1);
            }

            return result;
        }

        private float Distance(PointF a, PointF b)
            => (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
    }
}
