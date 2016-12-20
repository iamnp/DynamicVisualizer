using DynamicVisualizer.Expressions;

namespace DynamicVisualizer.Figures
{
    public class Magnet
    {
        public readonly string Def;
        public readonly ScalarExpression X;
        public readonly ScalarExpression Y;

        public Magnet(ScalarExpression x, ScalarExpression y, string def)
        {
            X = x;
            Y = y;
            Def = def;
        }

        public bool EqualExprStrings(Magnet a)
        {
            return X.ExprString == a.X.ExprString && Y.ExprString == a.Y.ExprString;
        }
    }
}