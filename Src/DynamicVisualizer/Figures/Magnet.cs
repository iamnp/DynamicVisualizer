using DynamicVisualizer.Expressions;

namespace DynamicVisualizer.Figures
{
    public class Magnet
    {
        public string Definition;
        public ScalarExpression X;
        public ScalarExpression Y;

        public Magnet(ScalarExpression x, ScalarExpression y, string definition)
        {
            X = x;
            Y = y;
            Definition = definition;
        }

        public bool EqualExprStrings(Magnet a)
        {
            return X.ExprString == a.X.ExprString && Y.ExprString == a.Y.ExprString;
        }
    }
}