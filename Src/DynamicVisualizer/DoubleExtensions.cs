namespace DynamicVisualizer
{
    public static class DoubleExtensions
    {
        public const string DoubleFixedPoint = "0.##########";
        public const double Tolerance = 1e-10;

        public static string Str(this double d)
        {
            return d.ToString(DoubleFixedPoint);
        }
    }
}