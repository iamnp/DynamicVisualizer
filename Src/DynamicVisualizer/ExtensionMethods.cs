using System.Globalization;

namespace DynamicVisualizer
{
    public static class ExtensionMethods
    {
        public static string Str(this double d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }
    }
}