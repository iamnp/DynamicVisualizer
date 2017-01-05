using System;
using System.Windows;

namespace DynamicVisualizer
{
    public static class Utils
    {
        public const string DoubleFixedPoint = "0.##########";
        public const double Tolerance = 1e-10;

        public static string Str(this double d)
        {
            return d.ToString(DoubleFixedPoint);
        }

        public static Point Move(this Point p, int x, int y)
        {
            return new Point(p.X + x, p.Y + y);
        }

        public static double AngleBetween(double ax, double ay, double bx, double by)
        {
            var angle = Math.Acos((ax * bx + ay * by)
                                  / (Math.Sqrt(ax * ax + ay * ay) * Math.Sqrt(bx * bx + by * by)));
            if (ax * by - ay * bx < 0)
            {
                return angle;
            }
            return -angle;
        }

        public static bool PointSector(Point p, Point center)
        {
            var a = AngleBetween(p.X - center.X, p.Y - center.Y, 1, 0);
            return ((a > Math.PI / 4.0) && (a < 3.0 * Math.PI / 4.0))
                   || ((a < -Math.PI / 4.0) && (a > -3.0 * Math.PI / 4.0));
        }
    }
}