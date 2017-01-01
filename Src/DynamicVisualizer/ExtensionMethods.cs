using System.Windows;

namespace DynamicVisualizer
{
    public static class ExtensionMethods
    {
        public const string DoubleFixedPoint = "0.##########";

        public static string Str(this double d)
        {
            return d.ToString(DoubleFixedPoint);
        }

        public static Point Move(this Point p, int x, int y)
        {
            return new Point(p.X + x, p.Y + y);
        }
    }
}