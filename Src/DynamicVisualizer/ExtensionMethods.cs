using System.Globalization;
using System.Windows;

namespace DynamicVisualizer
{
    public static class ExtensionMethods
    {
        public const string DoubleFixedPoint = "0.##########";

        public static string Str(this double d)
        {
            if ((d < 1.0) && (d > -1.0))
            {
                return d.ToString(DoubleFixedPoint);
            }
            return d.ToString(CultureInfo.InvariantCulture);
        }

        public static Point Move(this Point p, int x, int y)
        {
            return new Point(p.X + x, p.Y + y);
        }
    }
}