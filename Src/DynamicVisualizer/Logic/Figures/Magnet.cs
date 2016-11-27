using DynamicVisualizer.Logic.Expressions;

namespace DynamicVisualizer.Logic.Figures
{
    public class Magnet
    {
        public ScalarExpression X;
        public ScalarExpression Y;

        public Magnet(ScalarExpression x, ScalarExpression y)
        {
            X = x;
            Y = y;
        }
    }
}