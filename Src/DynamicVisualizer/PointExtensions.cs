using System.Windows;

namespace DynamicVisualizer
{
    internal static class PointExtensions
    {
        public static Point Move(this Point p, int x, int y)
        {
            return new Point(p.X + x, p.Y + y);
        }
    }
}