using System.Globalization;
using System.Windows;

namespace DynamicVisualizer
{
    public static class ExtensionMethods
    {
        public static string Str(this double d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }

        public static Point Move(this Point p, int x, int y)
        {
            return new Point(p.X + x, p.Y + y);
        }
    }
}