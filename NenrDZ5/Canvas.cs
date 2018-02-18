using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NenrDZ5
{
    public class Canvas : PictureBox
    {
        private bool _isDrawing;
        private Point _lastPoint;
        private Graphics _g;
        private List<Point> _points;

        public Canvas()
        {
            MouseDown += Canvas_MouseDown;
            MouseUp += Canvas_MouseUp;
            MouseMove += Canvas_MouseMove;
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            _isDrawing = false;
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            Size size = Size;
            Image = new Bitmap(size.Width, size.Height);
            Refresh();
            _g = Graphics.FromImage(Image);

            _points = new List<Point>();
            _lastPoint = new Point(e.X, e.Y);
            _points.Add(_lastPoint);
            _isDrawing = true;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Pen pen = new Pen(Color.DodgerBlue);
            if (!_isDrawing) return;

            _g.DrawLine(pen, _lastPoint, e.Location);
            _lastPoint = e.Location;
            _points.Add(_lastPoint);
            Refresh();
        }

        public List<PointF> GetPoints()
        {
            PointF center = new PointF(
                (float)_points.Average(p => p.X),
                (float)_points.Average(p => p.Y)
            );

            var points = _points.Select(p => new PointF(p.X - center.X, p.Y - center.Y)).ToList();

            float maxX = points.Max(p => Math.Abs(p.X));
            float maxY = points.Max(p => Math.Abs(p.Y));
            float scale = Math.Max(maxX, maxY);

            return points.Select(p => new PointF(p.X / scale, p.Y / scale)).ToList();
        }
    }
}
